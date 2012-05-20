using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

/*public class MainForm : Form
{
    private SeekForm sf;
    private ResultForm rf;

    public MainForm()
    {
        sf = new SeekForm();
        rf = new ResultForm();
    }

    public void SetSeek()
    {
        this = sf;
        this.Refresh();
    }

    public void SetResult()
    {
        this = rf;
        this.Refresh();
    }
}*/

public class SeekForm : Form
{
    private TextBox tb;
    private int BORDERPADDING = 40,
    PANEL_SPACE = 8;

    public SeekForm()
    {
      InitUI();

      Text = "Kapalgrunnur";
      Size = new Size(300,300);
      CenterToScreen();
    }

    void InitUI()
    {
      MenuStrip ms = new MenuStrip();
      ToolStripMenuItem file = new ToolStripMenuItem("&File");
      ToolStripMenuItem newDB = new ToolStripMenuItem(
                  "&New", null, new EventHandler(OnNew));
      ToolStripMenuItem exit = new ToolStripMenuItem(
                  "E&xit", null, new EventHandler(OnExit));
      Button btnOK = new Button();
      int PANEL_HEIGHT = btnOK.Height + PANEL_SPACE;
      tb = new TextBox();
      Panel btnPnl = new Panel();

      newDB.ShortcutKeys = Keys.Control | Keys.N;
      file.DropDownItems.Add(newDB);
      file.DropDownItems.Add(exit);

      ms.Parent = this;
      ms.Items.Add(file);
      MainMenuStrip = ms;
      
      tb.Parent = this;
      tb.Width = this.Width-BORDERPADDING;
      int X = (this.Width - tb.Width)/2;
      int Y = (this.Height - tb.Height)/2;
      tb.Location = new Point(X,Y);

      btnPnl.Parent = this;
      btnPnl.Dock = DockStyle.Bottom;

      X += btnOK.Width;
      Y = (PANEL_HEIGHT - btnOK.Height) / 2;

      btnOK.Text = "Velja";
      btnOK.Parent = btnPnl;
      btnOK.Location = new Point(this.Width-X, Y);
      btnOK.Click += new EventHandler(OkClicked);
    }

    void OkClicked(object sender, EventArgs e)
    {
        DataSet ds = sqlApi.LookupCable(tb.Text);
        Console.WriteLine(ds);
    }

    void OnNew(object sender, EventArgs e)
    {
        using(InputDialog diag = new InputDialog())
        {
            if(diag.ShowDialog() == DialogResult.OK)
            {
                sqlApi.InitDB(diag.GetText());
            }
        }
    }

    void OnExit(object sender, EventArgs e)
    {
      Close();
    }
}

public class ResultForm : Form
{
    public ResultForm(object datasource)
    {
      MenuStrip ms = new MenuStrip();
      ToolStripMenuItem file = new ToolStripMenuItem("&File");
      ToolStripMenuItem exit = new ToolStripMenuItem(
                  "E&xit", null, new EventHandler(OnExit));
      DataGridView dgv = new DataGridView();

      file.DropDownItems.Add(exit);
      ms.Parent = this;
      ms.Items.Add(file);
      MainMenuStrip = ms;

      dgv.Parent = this;
      dgv.Dock = DockStyle.Fill;

      dgv.DataSource = datasource;
    }

    void OnExit(object sender, EventArgs e)
    {
      Close();
    }
}

public class InputDialog : Form
{
    private TextBox tb;
    //private int BORDERPADDING = 0,
    //PANEL_SPACE = 8;

    public InputDialog()
    {
        InitUI();

        Size = new Size(200,100);
        Text = "Input dialog";
        CenterToScreen();
    }

    void InitUI()
    {
        Button btn = new Button();
        tb = new TextBox();

        tb.Parent = this;
        btn.Parent = this;

        tb.Dock = DockStyle.Fill;
        btn.Dock = DockStyle.Bottom;

        btn.Text = "Velja";
        btn.Click += new EventHandler(OnClicked);
    }

    void OnClicked(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    public string GetText()
    {
        return tb.Text;
    }
}

public class Keyrsla
{
    public static void Main(string[] args)
    {
       Application.Run(new SeekForm());
    }
}


