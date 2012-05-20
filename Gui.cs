using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

public class MainForm : Form
{
    private TextBox tb;
    private DataGridView dgv;

    public MainForm()
    {
        // Build components
        AddMenu();
        InitUI();

        CenterToScreen();
        Text = "Kapalgrunnur";
        Size = new Size(500,500);
    }

    void InitUI()
    {
        FlowLayoutPanel inputPanel = new FlowLayoutPanel();
        FlowLayoutPanel wholePanel = new FlowLayoutPanel();
        Button btnOK = new Button();
        tb = new TextBox();
        dgv = new DataGridView();
         
        // panel settings.
        inputPanel.FlowDirection = FlowDirection.LeftToRight;
        wholePanel.FlowDirection = FlowDirection.TopDown;
        wholePanel.Parent = this;
        wholePanel.Dock = DockStyle.Fill;
        wholePanel.Controls.Add(inputPanel);

        // tb settings.
        tb.Parent = inputPanel;

        // btnOK settings.
        btnOK.Parent = inputPanel;
        btnOK.Text = "Velja";
        btnOK.Click += new EventHandler(OnSelect);


        // dgv settings.
        wholePanel.Controls.Add(dgv);
    }

    void AddMenu()
    {
        MainMenu menu = new MainMenu();
        this.Menu = menu;

        MenuItem file = new MenuItem("&File");
        MenuItem newDB = new MenuItem("&New");
        MenuItem exit = new MenuItem("E&xit");
        
        exit.Click += new EventHandler(OnExit);
        newDB.Click += new EventHandler(OnNew);

        file.MenuItems.Add(newDB);
        file.MenuItems.Add(exit);

        menu.MenuItems.Add(file);
    }

    void OnSelect(object sender, EventArgs e)
    {
        DataSet ds = sqlApi.LookupCable(tb.Text);
        dgv.DataSource = ds.Tables["Cable"];
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

public class InputDialog : Form
{
    private TextBox tb;

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
        Application.Run(new MainForm());
    }
}


