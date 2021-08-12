using System;

namespace GridViewSample.Common
{
    public class MergebleGridBoxConfig
    {
        public MergebleGridBoxConfig()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="rowSize">行サイズ</param>
        /// <param name="columnSize">列サイズ</param>
        /// <param name="mergeHorizontal">セルの結合（水平方向）</param>
        /// <param name="mergeVertical">セルの結合（垂直方向）</param>
        /// <param name="mergeColCount">セル結合を有効にする最後の列のインデックスを設定する</param>
        public MergebleGridBoxConfig(double rowSize, double[] columnSize, bool mergeHorizontal, bool mergeVertical, int mergeColCount)
        {
            RowSize = rowSize;
            ColumnSize = columnSize;
            MergeHorizontal = mergeHorizontal;
            MergeVertical = mergeVertical;
            MergeColCount = mergeColCount;
        }

        /// <summary>
        /// 行サイズ
        /// </summary>
        public double RowSize = double.NaN;

        /// <summary>
        /// 列サイズ
        /// </summary>
        public double[] ColumnSize = Array.Empty<double>();

        /// <summary>
        /// セルの結合（水平方向）
        /// 右方向に結合するか
        /// </summary>
        public bool MergeHorizontal = false;

        /// <summary>
        /// セルの結合（垂直方向）
        /// 下方向に結合するか
        /// </summary>
        public bool MergeVertical = false;

        /// <summary>
        /// セル結合を有効にする最後の列のインデックスを設定する
        /// 制限しない場合「-1」を設定
        /// </summary>
        public int MergeColCount = -1;
    }
}
