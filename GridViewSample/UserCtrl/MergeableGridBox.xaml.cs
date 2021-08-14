using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GridViewSample.UserCtrl
{
    /// <summary>
    /// MargableGridBox.xaml の相互作用ロジック
    /// </summary>
    public partial class MargableGridBox : UserControl
    {
        [Browsable(true)]
        [Description("グリッドボックス背景色")]
        public Color BackColor { get; set; } = Colors.White;
        [Browsable(true)]
        [Description("グリッドボックス前景色")]
        public Color ForeColor { get; set; } = Colors.Black;

        [Browsable(true)]
        [Description("スクロールしない固定行数")]
        public int FrozenCount { get; set; } = 0;

        [Browsable(true)]
        [Description("グリッドボックス設定情報")]
        public MergebleGridBoxConfig GridBoxConfig { get; set; } = new MergebleGridBoxConfig();

        [Browsable(true)]
        [Description("データソース")]
        public DataTable DataSource { get; set; } = new DataTable();


        public MargableGridBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表示更新
        /// </summary>
        public void Update()
        {
            // 列サイズの設定がされていない場合処理しない
            if (GridBoxConfig.ColumnSize.Length <= 0)
            {
                return;
            }

            // スクロールしない固定領域のサイズを取得・設定する
            double frozenSize = GridBoxConfig.ColumnSize.Take(FrozenCount).Sum();
            // スクロール領域のサイズを取得・設定する
            double noFrozenSize = GridBoxConfig.ColumnSize.Skip(FrozenCount).Sum();

            // データ行情報を列挙 
            DataRow[] dataRowArray = DataSource.Rows.Cast<DataRow>().ToArray();

            for (int row = 0; row < dataRowArray.Length; row++)
            {
                int mergeRowCount = 1;

                // 素直方向に結合するか
                if (GridBoxConfig.MergeVertical && GridBoxConfig.MergeColCount != 0 && (row + 1) < dataRowArray.Length)
                {
                    foreach (DataRow dRow in dataRowArray.Skip(row + 1))
                    {
                        if ((string)dRow[0] != (string)dataRowArray[row].ItemArray[0])
                        {
                            break;
                        }

                        // 値が同じ行をカウント
                        mergeRowCount++;
                    }
                }

                // アイテム作成
                MergeableGridItem gridItem = new MergeableGridItem()
                {
                    Height = GridBoxConfig.RowSize * mergeRowCount,
                    FrozenWidth = frozenSize,
                    NoFrozenWidth = noFrozenSize,
                    BackColor = BackColor,
                    ForeColor = ForeColor,
                    FrozenCount = FrozenCount,
                    GridBoxConfig = GridBoxConfig,
                    DataSource = dataRowArray.Skip(row).Take(mergeRowCount).ToArray()
                };
                gridItem.Update();

                // スタックパネルに追加
                ItemPanel.Children.Add(gridItem);

                // カウンターを調整
                row += mergeRowCount - 1;
            }
        }

        public void ScrollToHorizontal(double offset)
        {
            foreach (MergeableGridItem gridItem in ItemPanel.Children)
            {
                gridItem.ScrollToHorizontal(offset);
            }
        }
        public void ScrollToVertical(double offset)
        {
            VScrollBar.ScrollToVerticalOffset(offset);
        }


        /// <summary>
        /// クリックイベントをハンドルします。
        /// </summary>
        public event EventHandler<RoutedEventArgs> Click;
        private void Label_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Click?.Invoke(this, e);
        }



        // 選択位置取得？
        // 選択データ取得
    }
}
