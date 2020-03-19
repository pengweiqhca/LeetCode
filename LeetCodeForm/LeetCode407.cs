using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeetCodeForm
{
    public partial class LeetCode407 : Form
    {
        private readonly DataGridView _dataGrid;

        public LeetCode407(int[][] heightMap)
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            SizeChanged += LeetCode407_SizeChanged;

            int xLength = heightMap.Length, yLength = heightMap.Max(m => m.Length);

            _dataGrid = new DataGridView();
            Controls.Add(_dataGrid);
            _dataGrid.Width = Width - 100;
            _dataGrid.Height = Height;

            for (var y = 0; y < yLength; y++)
            {
                _dataGrid.Columns.Add(y.ToString(), y.ToString());
                _dataGrid.Columns[y].SortMode = DataGridViewColumnSortMode.NotSortable;
                _dataGrid.Columns[y].Width = 80;
            }
            for (var x = 1; x < xLength; x++)
            {
                _dataGrid.Rows.Add();
            }

            for (var x = 0; x < xLength; x++)
            {
                var array = heightMap[x];
                for (var y = 0; y < yLength; y++)
                {
                    _dataGrid[x, y] = new PositionInfo(y >= array.Length ? 0 : array[y], x, y);
                }
            }
;
            DoubleClick += Button_Click;
        }

        private void LeetCode407_SizeChanged(object sender, EventArgs e)
        {
            _dataGrid.Width = Width - 100;
            _dataGrid.Height = Height - 100;
        }

        private async void Button_Click(object sender, EventArgs e) => await TrapRainWater();

        public async Task<int> TrapRainWater()
        {
            if (_dataGrid.RowCount < 3 || _dataGrid.ColumnCount < 3) return 0;

            await ComputeWall(_dataGrid, _dataGrid.RowCount, _dataGrid.ColumnCount);

            return _dataGrid.Rows.OfType<DataGridViewRow>().SelectMany(row => row.Cells.OfType<PositionInfo>())
                   .Where(p => p.Height > p.Value)
                   .Select(p => p.Height - p.Value)
                   .Sum();
        }

        async Task ComputeWall(DataGridView heightMap, int xLength, int yLength)
        {
            for (var x = 1; x < xLength - 1; x++)
                for (var y = 1; y < yLength - 1; y++)
                {
                    var position = (PositionInfo)heightMap[x, y];
                    if (position.Height < 1)
                        await ComputeWall(heightMap, xLength, yLength, null, new SortedSet<PositionInfo>(), new SortedSet<PositionInfo>(new PositionInfoValueComparer()), position);
                }
        }

        async Task<int> ComputeWall(DataGridView heightMap, int xLength, int yLength, int? minHeight,
               ISet<PositionInfo> computing,
               ISet<PositionInfo> edges,
               PositionInfo ll)
        {
            edges.Add((PositionInfo)heightMap[ll.X, ll.Y - 1]);
            edges.Add((PositionInfo)heightMap[ll.X, ll.Y + 1]);
            edges.Add((PositionInfo)heightMap[ll.X - 1, ll.Y]);
            edges.Add((PositionInfo)heightMap[ll.X + 1, ll.Y]);

            computing.Add(ll);

            var lowLying = new HashSet<PositionInfo>();
            var height = await ComputePoint(heightMap, xLength, yLength, minHeight, computing, edges, lowLying);
            foreach (var position in lowLying)
            {
                if (position.Height < height) position.Height = height;
            }
            return height;
        }

        async Task<int> ComputePoint(DataGridView heightMap, int xLength, int yLength, int? minHeight,
              ISet<PositionInfo> computing,
              ISet<PositionInfo> edges,
              ISet<PositionInfo> lowLying)
        {
            while (true)
            {
                var min = edges.FirstOrDefault(p => !computing.Contains(p));
                if (min == null) return int.MaxValue;

                if (minHeight.HasValue && min.Value >= minHeight) return minHeight.Value;

                foreach (var e in edges)
                {
                    e.Selected = false;
                    e.Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                }

                foreach (var e in lowLying)
                {
                    e.Selected = false;
                    e.Style = new DataGridViewCellStyle { ForeColor = Color.Green };
                }

                min.Selected = true;
                //await Task.Delay(500);
                var height = await ComputeHeight(heightMap, xLength, yLength, min, computing, edges, lowLying);
                if (height > 0)
                {
                    if (height > minHeight.GetValueOrDefault(height))
                    {
                        foreach (var ll in lowLying)
                        {
                            if (ll.Height > 0 && ll.Height != height) throw new Exception($"高错误：{ll}:{ll.Height}新{height}");
                            if (ll.Value < height) ll.Height = height;
                        }
                    }

                    return height;
                }
            }
        }

        async Task<int> ComputeHeight(DataGridView heightMap, int xLength, int yLength, PositionInfo position,
            ISet<PositionInfo> computing,
            ISet<PositionInfo> edges,
            ISet<PositionInfo> lowLying)
        {
            if (position.X < 1 || position.Y < 1 || position.X > xLength - 2 || position.Y > yLength - 2) return position.Value;

            computing.Add(position);

            if (position.Height >= position.Value) return position.Height;

            var list = new List<int>();
            var e1 = new SortedSet<PositionInfo>();
            var l1 = new List<PositionInfo>();

            foreach (var b in new[] { new[] { 0, -1 }, new[] { 0, 1 }, new[] { -1, 0 }, new[] { 1, 0 } })
            {
                var p = (PositionInfo)heightMap[position.X + b[0], position.Y + b[1]];
                if (computing.Contains(p)) continue;

                if (position.Value <= p.Value)
                {
                    edges.Add(p);
                }
                else
                {
                    if (p.X < 1 || p.Y < 1 || p.X > xLength - 2 || p.Y > yLength - 2)
                    {
                        if (position.Value > p.Value) list.Add(position.Value);
                    }
                    else
                    {
                        var e2 = new SortedSet<PositionInfo>(new PositionInfoValueComparer());
                        var l2 = new SortedSet<PositionInfo>();

                        if (position.Value > await ComputeWall(heightMap, xLength, yLength, position.Value, computing, e2, p))
                        {
                            list.Add(position.Value);
                        }
                        else
                        {
                            foreach (var a in e2) if (!l1.Contains(a)) e1.Add(a);
                            foreach (var a in l2) if (!e1.Contains(a)) l1.Add(a);
                        }
                    }
                }
            }

            if (list.Count > 0) return list.Min();

            foreach (var a in e1) edges.Add(a);
            foreach (var a in l1) lowLying.Add(a);

            lowLying.Add(position);
            edges.Remove(position);

            return -1;
        }

        class PositionInfo : DataGridViewTextBoxCell, IComparable<PositionInfo>, IComparable
        {
            private int _height;

            public PositionInfo(int value, int x, int y)
            {
                Value = value;
                base.Value = $"{value}";
                X = x;
                Y = y;
                ToolTipText = $"{value}:{Height}";
            }

            public new int Value { get; }
            public int X { get; }
            public int Y { get; }

            public int Height
            {
                get => _height;
                set
                {
                    _height = value;
                    ToolTipText = $"{Value}:{Height}";
                }
            }

            public override string ToString() => $"{X},{Y},{Value}";

            public int CompareTo(PositionInfo other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (other is null) return 1;
                var xComparison = X.CompareTo(other.X);
                if (xComparison != 0) return xComparison;
                return Y.CompareTo(other.Y);
            }

            public int CompareTo(object obj)
            {
                if (obj is null) return 1;
                if (ReferenceEquals(this, obj)) return 0;
                return obj is PositionInfo other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(PositionInfo)}");
            }

            public override bool Equals(object obj) => CompareTo(obj) == 0;

            public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
        }

        class PositionInfoValueComparer : IComparer<PositionInfo>
        {
            public int Compare(PositionInfo x, PositionInfo y)
            {
                if (x is null) return y is null ? 0 : -1;

                if (y is null) return 1;

                if (ReferenceEquals(x, y)) return 0;

                var result = x.Value - y.Value;
                if (result != 0) return result;

                result = x.X - y.X;
                if (result != 0) return result;

                return x.Y - y.Y;
            }
        }
    }
}
