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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            //ItemPanel
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
