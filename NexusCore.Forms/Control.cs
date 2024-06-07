using System.Reflection;

namespace NexusCore.Forms {


    public class MenuItem {
        public string Name { get; set; }
        public string Link { get; set; }
        public bool IsLink { get; set; }

        public MenuItem(string name, string? link = null) {
            Name = name;
            Link = link;
            IsLink = link != null;
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

    public class MenuTable {
        public List<MenuRow> Rows { get; private set; }
        private readonly Dictionary<int, List<MenuRow>> _pageCache;
        private readonly int _columnCount;
        private readonly int _pageSize;
        private bool _updating;

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
            if (row.Cells.Count == _columnCount) {
                Rows.Add(row);
            } else {
                throw new ArgumentException("Row must have the correct number of columns.");
            }
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

        public void FetchData(int start, int count) {
            for (int i = start; i < start + count; i++) {
                MenuRow row = new(_columnCount);
                for (int j = 0; j < _columnCount; j++) {
                    row.SetCell(j, new MenuItem($"Cell {i + 1}-{j + 1}"));
                }
                AddRow(row);
            }
        }

        public void SelectCellRange(int startRow, int startCell, int endRow, int endCell) {
            if (_updating) {
                return;
            }

            for (int i = startRow; i <= endRow; i++) {
                for (int j = startCell; j <= endCell; j++) {
                    MenuItem cell = Rows[ i ].Cells[ j ];
                    Console.WriteLine($"Selected Cell: {cell.Name}");
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

        public void FillIn(List<PropertyInfo> columns) {
            Rows.Clear();

            foreach (NexusEF entity in entities) {

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
            table.FetchData(0, 10);

            List<MenuRow> page = table.GetPage(1);

            foreach (MenuRow row in page) {
                foreach (MenuItem cell in row.Cells) {
                    Console.WriteLine($"Cell: {cell.Name}, IsLink: {cell.IsLink}");
                }
            }

            table.ClearCache();
            table.FetchData(10, 10);
            List<MenuRow> nextPage = table.GetPage(2);

            foreach (MenuRow row in nextPage) {
                foreach (MenuItem cell in row.Cells) {
                    Console.WriteLine($"Cell: {cell.Name}, IsLink: {cell.IsLink}");
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
                    Console.WriteLine($"Cell: {cell.Name}, IsLink: {cell.IsLink}");
                }
            }
        }
    }
