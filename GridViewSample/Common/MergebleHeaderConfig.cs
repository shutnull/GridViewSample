using System;

namespace GridViewSample.Common
{
    public class MergeableHeaderConfig
    {
        /// <summary>
        /// ヘッダーアイテム設定
        /// </summary>
        public MergeableHeaderConfigItem[] Items = Array.Empty<MergeableHeaderConfigItem>();
    }

    public class MergeableHeaderConfigItem
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="text">ヘッダーに表示するテキスト</param>
        /// <param name="columnSize">列サイズ</param>
        /// <param name="rowSize">行サイズ</param>
        public MergeableHeaderConfigItem(string text, double columnSize, double rowSize)
        {
            Text = text;
            ColumnSize = columnSize;
            RowSize = rowSize;
        }

        /// <summary>
        /// ヘッダーに表示するテキスト
        /// </summary>
        public string Text = string.Empty;

        /// <summary>
        /// 列サイズ
        /// </summary>
        public double ColumnSize = double.NaN;

        /// <summary>
        /// 行サイズ
        /// </summary>
        public double RowSize = double.NaN;

        /// <summary>
        /// セルの結合（水平方向）
        /// 右方向に結合するセルの数を指定
        /// </summary>
        //public int ColSpan = 0;

        /// <summary>
        /// セルの結合（垂直方向）
        /// 下方向に結合するセルの数を指定
        /// </summary>
        //public int RowSpan = 0;
    }
}
