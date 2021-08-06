using GridViewSample.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media;

namespace GridViewSample.UserCtrl
{
    /// <summary>
    /// MargableDataGridView.xaml の相互作用ロジック
    /// </summary>
    public partial class MargableDataGridView : UserControl
    {
        private Color _BackColor = Color.FromArgb(0xFF, 0x80, 0x80, 0x80);
        [Browsable(true)]
        [Description("背景色")]
        public Color BackColor 
        { 
            get
            {
                return _BackColor;
            }
            set
            {
                _BackColor = value;
                SolidColorBrush colorBrush = new SolidColorBrush(_BackColor);
                Background = colorBrush;
                Header.Background = colorBrush;
                GridBox.Background = colorBrush;
            }
        }

        [Browsable(true)]
        [Description("ヘッダー背景色")]
        public Color HeaderBackColor { get; set; } = Color.FromArgb(0xFF, 0x01, 0x25, 0x97);
        [Browsable(true)]
        [Description("ヘッダー前景色")]
        public Color HeadeForeColor { get; set; } = Colors.White;

        [Browsable(true)]
        [Description("グリッドボックス背景色")]
        public Color GridBoxBackColor { get; set; } = Colors.White;
        [Browsable(true)]
        [Description("グリッドボックス前景色")]
        public Color GridBoxForeColor { get; set; } = Colors.Black;

        [Browsable(true)]
        [Description("スクロールしない固定行数")]
        public int FrozenCount { get; set; } = 0;

        [Browsable(true)]
        [Description("ヘッダーの設定情報リスト")]
        public List<MergeableHeaderConfig> HeaderConfigList { get; set; } = new List<MergeableHeaderConfig>(0);

        [Browsable(true)]
        [Description("グリッドボックス設定情報")]
        public MergebleGridBoxConfig GridBoxConfig { get; set; } = new MergebleGridBoxConfig();

        [Browsable(true)]
        [Description("データソース")]
        public DataTable DataSource { get; set; } = new DataTable();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MargableDataGridView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表示更新
        /// </summary>
        public void Update()
        {
            Header.HeaderBackColor = HeaderBackColor;
            Header.HeadeForeColor = HeadeForeColor;
            Header.FrozenCount = FrozenCount;
            Header.HeaderConfigList = HeaderConfigList;
            double hSize = Header.Update();
            HeaderRowSize.Height = new GridLength(hSize, GridUnitType.Pixel);

        }
    }
}
