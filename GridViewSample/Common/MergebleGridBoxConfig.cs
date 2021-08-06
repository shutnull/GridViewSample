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
        public MergebleGridBoxConfig(double rowSize, double[] columnSize, bool mergeHorizontal = false, bool mergeVertical = false)
        {
            RowSize = rowSize;
            ColumnSize = columnSize;
            MergeHorizontal = mergeHorizontal;
            MergeVertical = mergeVertical;
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
    }
}
