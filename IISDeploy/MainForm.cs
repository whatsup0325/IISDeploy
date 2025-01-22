using Microsoft.Web.Administration;
using Newtonsoft.Json;

namespace IISDeploy
{
    public partial class MainForm : Form
    {
        List<IISSite> sites = new List<IISSite>();
        IISManager IISManager = new IISManager();
        UserSettings settings = new UserSettings();
        public MainForm()
        {
            InitializeComponent();
            LoadSetting();
            InitIISSites();

            buildSelect.SelectedIndex = 0;
            IISListBox_SelectedIndexChanged(IISListBox, new EventArgs());
            IISManager.StatusChanged += (status) =>
            {
                AddStatus(status);
            };
        }

        private void AddStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    AddTextToTextBox(status);
                }));
            }
            else
            {
                AddTextToTextBox(status);
            }
        }

        private void AddTextToTextBox(string text)
        {
            StatusText.Text += text + Environment.NewLine;
            StatusText.SelectionStart = StatusText.Text.Length;
            StatusText.ScrollToCaret();
        }

        private void InitIISSites()
        {
            var tempSelectIndex = IISListBox.SelectedIndex;
            sites = IISManager.ListIISWebSites().Select(x => new IISSite() { Site = x }).ToList();
            IISListBox.DataSource = sites;

            if (tempSelectIndex >= 0)
            {
                IISListBox.SelectedIndex = tempSelectIndex;
            }

        }

        private void ControlIISSite(string siteName, string action)
        {
            ServerManager serverManager = new ServerManager();
            var site = serverManager.Sites[siteName];
            if (site == null)
            {
                AddTextToTextBox($"Site {siteName} not found.");
                return;
            }

            if (action == "stop" && site.State == ObjectState.Started)
            {
                site.Stop();
                AddTextToTextBox($"Site {siteName} stopped.");
            }
            else if (action == "start" && site.State == ObjectState.Stopped)
            {
                site.Start();
                AddTextToTextBox($"Site {siteName} started.");
            }
            else
            {
                AddTextToTextBox($"Site {siteName} is already in the desired state.");
            }
            InitIISSites();
        }

        public bool SetBuildStrategy()
        {
            if (buildSelect.SelectedItem is not string buildSelectString)
                return false;

            if (string.IsNullOrEmpty(OutputPathTextBox.Text))
            {
                MessageBox.Show("Please set the Output Path");
                return false;
            }

            if (buildSelectString == "NodeJs")
            {
                IISManager.UseNodeJsBuild(OutputPathTextBox.Text);
                return true;
            }
            else if (buildSelectString == ".Net")
            {
                if (string.IsNullOrEmpty(buildSelectString))
                {
                    MessageBox.Show("Please set the target project");
                    return false;
                }

                IISManager.UseDotNetBuild(TargetProjectTextBox.Text, OutputPathTextBox.Text);
                TargetProjectPanel.Visible = true;
                return true;
            }

            MessageBox.Show("Set Build Strategy Error");
            return false;
        }

        private void SetDefault()
        {
            if (buildSelect.SelectedItem is not string buildSelectString)
                return;

            TargetProjectPanel.Visible = false;
            if (buildSelectString == "NodeJs")
            {
                BranchTextBox.Text = "main";
                OutputPathTextBox.Text = "dist";
            }
            if (buildSelectString == ".Net")
            {
                TargetProjectPanel.Visible = true;
                BranchTextBox.Text = "master";
                OutputPathTextBox.Text = "bin\\Debug\\net6.0-windows";
            }



        }

        public void LoadSetting()
        {
            // 獲取 AppData 資料夾路徑
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, "IISDeploy");
            string settingsFile = Path.Combine(appFolder, "settings.json");

            // 確保應用程式資料夾存在
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            // 讀取設定檔
            if (File.Exists(settingsFile))
            {
                string json = File.ReadAllText(settingsFile);
                settings = JsonConvert.DeserializeObject<UserSettings>(json);
            }
        }
        public void SaveSetting()
        {
            // 獲取 AppData 資料夾路徑
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, "IISDeploy");
            string settingsFile = Path.Combine(appFolder, "settings.json");

            // 確保應用程式資料夾存在
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            //取代舊有設定
            var oldSettings = settings.webSettings.Where(x => x.webName != WebNameLabel.Text).ToList();
            settings.webSettings = oldSettings;

            // 新增新設定
            settings.webSettings.Add(new webSetting()
            {
                webName = WebNameLabel.Text,
                gitUrl = GitUrlTextBox.Text,
                branch = BranchTextBox.Text,
                buildStrategy = buildSelect.SelectedItem?.ToString() ?? "",
                outputPath = OutputPathTextBox.Text,
                targetProject = TargetProjectTextBox.Text,
            });

            // 儲存為 JSON
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(settingsFile, json);

            Console.WriteLine($"Settings saved to {settingsFile}");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SaveSetting();
            StatusText.Text = "";
            SetBuildStrategy();
            Task.Run(() => IISManager.DeployWithoutPause(GitUrlTextBox.Text, WebNameLabel.Text, FilePathTextBox.Text, BranchTextBox.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveSetting();
            StatusText.Text = "";

            Task.Run(() =>
                IISManager.DeployWithPause(GitUrlTextBox.Text, WebNameLabel.Text, FilePathTextBox.Text, BranchTextBox.Text)
            );


        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveSetting();
            StatusText.Text = "";
            ControlIISSite(WebNameLabel.Text, "stop");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveSetting();
            StatusText.Text = "";
            ControlIISSite(WebNameLabel.Text, "start");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveSetting();
        }

        private void buildSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (buildSelect.SelectedItem is string a)
            {
                SetDefault();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IISListBox.DrawMode = DrawMode.OwnerDrawFixed;
            IISListBox.ItemHeight = 50;
        }

        private void IISListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush roomsBrush;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds,
                    e.Index, e.State ^ DrawItemState.Selected, e.ForeColor, Color.FromArgb(136, 142, 154));

                roomsBrush = Brushes.Black;
            }
            else
            {
                roomsBrush = Brushes.Black;
            }

            //var linePen = new Pen(Brushes.Black);
            //var lineStartPoint = new Point(e.Bounds.Left, e.Bounds.Height + e.Bounds.Top);
            //var lineEndPoint = new Point(e.Bounds.Width, e.Bounds.Height + e.Bounds.Top);

            //e.Graphics.DrawLine(linePen, lineStartPoint, lineEndPoint);

            e.DrawBackground();

            if (e.Index < 0)
                return;

            var dataItem = IISListBox.Items[e.Index] as IISSite;

            var webNameFont = new Font("微軟正黑體", 12f, FontStyle.Bold);
            var statusFont = new Font("微軟正黑體", 8.25f, FontStyle.Regular);
            var urlFont = new Font("微軟正黑體", 8.25f, FontStyle.Regular);

            e.Graphics.DrawString(dataItem.Site.Name, webNameFont, Brushes.Black, e.Bounds.Left + 3, e.Bounds.Top + 5);

            string siteState = "";
            try
            {
                siteState = dataItem.Site.State.ToString();

            }
            catch
            {
                siteState = "Stopped";
            }

            e.Graphics.DrawString("Stats:" + siteState, statusFont, roomsBrush, e.Bounds.Left + 3, e.Bounds.Top + 24);
            e.Graphics.DrawString(dataItem.GitUrl.ToString(), urlFont, roomsBrush, e.Bounds.Left + 3, e.Bounds.Top + 40);
        }

        private void IISListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(sender is ListBox listBox))
                return;

            if (!(listBox.SelectedItem is IISSite item))
                return;

            var setting = settings.webSettings.FirstOrDefault(x => x.webName == item.Site.Name);


            SetDefault();
            WebNameLabel.Text = item.Site.Name;
            GitUrlTextBox.Text = setting?.gitUrl ?? "";
            FilePathTextBox.Text = Environment.ExpandEnvironmentVariables(item.Site.Applications["/"].VirtualDirectories["/"].PhysicalPath);
            buildSelect.SelectedItem = setting?.buildStrategy ?? "";
            if (!string.IsNullOrEmpty(setting?.targetProject))
                TargetProjectTextBox.Text = setting.targetProject;

            if (!string.IsNullOrEmpty(setting?.outputPath))
                OutputPathTextBox.Text = setting.outputPath;

            if (!string.IsNullOrEmpty(setting?.branch))
                BranchTextBox.Text = setting.branch;

            SetBuildStrategy();
        }
    }



    public class IISSite
    {
        public Site Site { get; set; } = null!;
        public string GitUrl { get; set; } = "";
    }
}
