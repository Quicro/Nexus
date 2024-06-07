using NexusCore.Components.Controller;
using NexusCore.Interfaces;
using NexusCore.Interfaces.Widgets;
using NexusEF;
using NexusEF.Models;
using System.Drawing;
using System.Reflection;
using static NexusCore.Helper;

namespace NexusCore.Widgets {

    public class MenuItem {
        private Packet packet;
        private Font fontItem;
        private Color foreColor;
        private string? textItem;

        public MenuItem(Packet packet, Font fontItem, Color foreColor, string? textItem) {
            this.packet = packet;
            this.fontItem = fontItem;
            this.foreColor = foreColor;
            this.textItem = textItem;
        }
    }

    public class MenuRow {
        public List<MenuItem> Cells { get; private set; }
        private readonly int _columnCount;
        public bool IsSelected { get; private set; }

        public MenuRow(int columnCount) {
            _columnCount = columnCount;
            Cells = new List<MenuItem>(new MenuItem[ columnCount ]);
        }

        public void SetCell(int index, MenuItem cell) {
            Cells[ index ] = index >= 0 && index < _columnCount ? cell : throw new IndexOutOfRangeException("Index exceeds the column count.");
        }

        public void SelectRow() {
            IsSelected = true;
        }

        public void DeselectRow() {
            IsSelected = false;
        }
    }

    public class MenuTable : IPacketSender, IElementWidget {
        public List<MenuRow> Rows { get; private set; }
        private readonly Dictionary<int, List<MenuRow>> _pageCache;
        private readonly int _columnCount;
        private readonly int _pageSize;
        private bool _updating;

        public event EventHandler<Packet> sent;

        public MenuTable(int columnCount, int pageSize) {
            Rows = [];
            _pageCache = [];
            _columnCount = columnCount;
            _pageSize = pageSize;
            _updating = false;
        }

        public void BeginUpdate() {
            _updating = true;
        }

        public void EndUpdate() {
            _updating = false;
            // Optional: Refresh the display or re-render if necessary
        }

        public void AddRow(MenuRow row) {
           // if (row.Cells.Count == _columnCount) {
                Rows.Add(row);
            //} else {
            //    throw new ArgumentException("Row must have the correct number of columns.");
            //}
        }

        public List<MenuRow> GetPage(int pageNumber) {
            if (_pageCache.ContainsKey(pageNumber)) {
                return _pageCache[ pageNumber ];
            }

            List<MenuRow> page = Rows.Skip(( pageNumber - 1 ) * _pageSize).Take(_pageSize).ToList();
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
                    MenuItem cell = Rows[ i ].Cells[ j ];
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
            Rows.Clear();

            foreach (INexusEntity entity in entities) {
                MenuRow row = new(columnCount: columns.Count);

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
                    }

                    MenuItem menuItem = new MenuItem(packet, fontItem, foreColor, textItem);
                    row.Cells.Add(menuItem);
                }

                AddRow(row);
            }
        }

        // Example usage
        public class Program {
            public static void gg() {
                int columnCount = 3;
                int pageSize = 2;

                MenuTable table = new(columnCount, pageSize);

                table.BeginUpdate();

                // Fetch initial data

                List<MenuRow> page = table.GetPage(1);

                foreach (MenuRow row in page) {
                    foreach (MenuItem cell in row.Cells) {
                    }
                }

                table.ClearCache();
                List<MenuRow> nextPage = table.GetPage(2);

                foreach (MenuRow row in nextPage) {
                    foreach (MenuItem cell in row.Cells) {
                    }
                }

                table.EndUpdate();

                // Select and deselect rows
                table.SelectRow(1);
                table.DeselectRow(1);

                // Select cell range
                table.SelectCellRange(0, 0, 1, 1);

                // Delete selected rows
                table.SelectRow(0);
                table.DeleteSelectedRows();

                Console.WriteLine("Remaining rows:");
                foreach (MenuRow row in table.Rows) {
                    foreach (MenuItem cell in row.Cells) {
                    }
                }
            }
        }
    }
}
