using NexusCore.Components.Controller;
using NexusCore.Interfaces;
using NexusCore.Interfaces.Widgets;
using NexusEF;
using NexusEF.Models;
using System.Collections;
using System.Drawing;
using System.Reflection;
using static NexusCore.Helper;

namespace NexusCore.Widgets {


    public class WidgetTable : IPacketSender, IWidget {
        public List<WidgetTableRow> Rows { get; private set; }

        public string Title;
        public WidgetTableHeaderRow HeaderRow { get; private set; }

        private readonly Dictionary<int, List<WidgetTableRow>> _pageCache;
        private readonly int _columnCount;
        private readonly int _pageSize;
        private bool _updating;

        public event EventHandler<Packet> sent;

        public WidgetTable(string title, int columnCount, int pageSize) {
            Rows = [];
            _pageCache = [];
            _columnCount = columnCount;
            _pageSize = pageSize;
            _updating = false;
            this.Title = title;
        }

        public void BeginUpdate() {
            _updating = true;
        }

        public void EndUpdate() {
            _updating = false;
            // Optional: Refresh the display or re-render if necessary
        }

        public void AddRow(WidgetTableRow row) {
           // if (row.Cells.Count == _columnCount) {
                Rows.Add(row);
            //} else {
            //    throw new ArgumentException("Row must have the correct number of columns.");
            //}
        }

        public List<WidgetTableRow> GetPage(int pageNumber) {
            if (_pageCache.ContainsKey(pageNumber)) {
                return _pageCache[ pageNumber ];
            }

            List<WidgetTableRow> page = Rows.Skip(( pageNumber - 1 ) * _pageSize).Take(_pageSize).ToList();
            _pageCache[ pageNumber ] = page;
            return page;
        }

        public void ClearCache() {
            _pageCache.Clear();
        }

        public void SelectCellRange(int startRow, int startCell, int endRow, int endCell) {
            if (_updating) {
                return;
            }

            for (int i = startRow; i <= endRow; i++) {
                for (int j = startCell; j <= endCell; j++) {
                    WidgetTableCell cell = Rows[ i ].Cells[ j ];
                }
            }
        }

        public void SelectRow(int rowIndex) {
            if (_updating) {
                return;
            }

            if (rowIndex >= 0 && rowIndex < Rows.Count) {
                Rows[ rowIndex ].SelectRow();
                Console.WriteLine($"Row {rowIndex + 1} selected.");
            } else {
                throw new IndexOutOfRangeException("Row index out of range.");
            }
        }

        public void DeselectRow(int rowIndex) {
            if (_updating) {
                return;
            }

            if (rowIndex >= 0 && rowIndex < Rows.Count) {
                Rows[ rowIndex ].DeselectRow();
                Console.WriteLine($"Row {rowIndex + 1} deselected.");
            } else {
                throw new IndexOutOfRangeException("Row index out of range.");
            }
        }

        public void DeleteSelectedRows() {
            if (_updating) {
                return;
            }

            _ = Rows.RemoveAll(row => row.IsSelected);
        }

        public void FillIn(List<PropertyInfo> columns, IEnumerable<INexusEntity> entities, Type type) {
            foreach (INexusEntity entity in entities) {
                WidgetTableRow row = new(columnCount: columns.Count, false);

                foreach (PropertyInfo column in columns) {
                    object value = column.GetValue(entity);
                    Type columnType = column.PropertyType;
                    IQueryable<INexusEntity> query = Helper.getQuery(type).Where(e => e.Id == entity.Id);
                    Packet packet = Packet.Create<EditorController, PacketSingleEditor>(type, query, this);
                    string textItem = "error";
                    Font fontItem = Fonts.fontDefault;
                    Color foreColor = Color.Black;

                    if (value is null) {
                        textItem = "NULL";
                        fontItem = Fonts.fontNull;
                        foreColor = Color.Gray;
                    } else {
                        bool list = isList(value);
                        bool subTypeOfEntity = isSubTypeOfEntity(columnType);
                        bool listOf = isListOf<INexusEntity>(value);

                        PacketRelationshipType packetRelationshipType = getPacketRelationshipType(list, subTypeOfEntity, listOf);

                        if (packetRelationshipType == PacketRelationshipType.Dummy) {
                            textItem = value.ToString();
                        }

                        if (packetRelationshipType == PacketRelationshipType.Single) {
                            if (column.Name.EndsWith("Id") && column.Name != "Id") {
                                PropertyInfo reference = type.GetProperties().Where(c => c.Name == column.Name.Replace("Id", "")).Single();
                                Type referenceType = reference.PropertyType;
                                int refID = (int)value;

                                IQueryable<INexusEntity> queryOfRelatedEntities = (IQueryable<INexusEntity>)callStaticGenericMethod(
                                    typeof(Extentions),
                                    nameof(Extentions.getQueryableByID),
                                    new Type[] { referenceType, type },
                                    new object[] { entity, refID }
                                );

                                packet = Packet.Create<ViewerController, PacketSingle>(referenceType, queryOfRelatedEntities, this);
                                textItem = referenceType.Name;
                                fontItem = Fonts.fontReference;
                                foreColor = Color.Blue;
                            }
                        }

                        if (packetRelationshipType == PacketRelationshipType.Array) {
                            Type referenceType = getListType(value);

                            referenceType = Extentions.getPossibleMoreMoreRelationType(type, referenceType) ?? referenceType;

                            IQueryable<INexusEntity> queryOfRelatedEntities = (IQueryable<INexusEntity>)callStaticGenericMethod(
                                typeof(Extentions),
                                nameof(Extentions.getRelatedQueryableByID),
                                new Type[] { type, referenceType },
                                new object[] { entity }
                            );

                            packet = Packet.Create<ViewerController, PacketArray>(referenceType, queryOfRelatedEntities, this);
                            fontItem = Fonts.fontReference;
                            foreColor = Color.Blue;
                            textItem = getListType(value).Name + "[]";
                        }

                        WidgetTableCell tableCell = new WidgetTableCell(packet, fontItem, foreColor, textItem);
                        row.Cells.Add(tableCell);
                    }

                }

                var rowPacketsHandlerEnums = row.Cells.Select(c => c.packet.handlerEnum).ToList();

                AddRow(row);
            }
        }

        internal void Headers(List<PropertyInfo> columns) {

            WidgetTableHeaderRow headerRow = new();
            foreach (PropertyInfo column in columns) {
                headerRow.Cells.Add(new WidgetTableHeaderCell(column.Name));
            }
            this.HeaderRow = headerRow;
        }

        public class WidgetTableHeaderCell {
            public string name;

            public WidgetTableHeaderCell(string name) => this.name = name;
        }
        public class WidgetTableHeaderRow{
            private int count;

            public WidgetTableHeaderRow() {
                this.count = count;
                Cells = new();
            }

            public List<WidgetTableHeaderCell> Cells { get; private set; }
        }

            public class WidgetTableCell {
            public  Packet packet;
            public  Font fontItem;
            public  Color foreColor;
            public  string? textItem;

            public WidgetTableCell(Packet packet, Font fontItem, Color foreColor, string? textItem) {
                this.packet = packet;
                this.fontItem = fontItem;
                this.foreColor = foreColor;
                this.textItem = textItem;
            }
        }

        public class WidgetTableRow {
            public List<WidgetTableCell> Cells { get; private set; }
            private readonly int _columnCount;
            private readonly bool isHeader;

            public bool IsSelected { get; private set; }

            public WidgetTableRow(int columnCount, bool isHeader) {
                _columnCount = columnCount;
                this.isHeader = isHeader;
                Cells = new List<WidgetTableCell>();
            }

            public void SelectRow() {
                IsSelected = true;
            }

            public void DeselectRow() {
                IsSelected = false;
            }
        }
    }
}
