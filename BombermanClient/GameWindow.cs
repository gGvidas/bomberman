using BombermanClasses;
using BombermanClasses.BombNameSpace;
using BombermanClasses.Items;
using BombermanClasses.Walls;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class   GameWindow : Form
    {
        bool IsGameOver = true;

        Int32 squareSize = 20;
        Int32 numSquaresX = 32;
        Int32 numSquaresY = 32;

        private Tile[][] world;

        private HubConnection hubConnection;

        Image img = null;
        Graphics imgGraph = null;
        Graphics graph = null;

        [NonSerialized]        
        private Image Background_;

        public GameWindow()
        {
            InitializeComponent();
            buttonStart.Enabled = false;
            InitConnection();
            this.screen.Visible = false;

            img = new Bitmap(squareSize * numSquaresX, squareSize * numSquaresY);
            imgGraph = Graphics.FromImage(img);
            graph = screen.CreateGraphics();

        }

    
        public void StateUpdated(string serializedWordFromServer)
        {
            var worldFromServer = JsonConvert.DeserializeObject<Tile[][]>(serializedWordFromServer, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            world = worldFromServer;
            Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textGameOver.Visible = false;

            this.timer1.Interval = 200;
            timer1.Start();
        }

        private void ChangeGameState()
        {
            IsGameOver = !IsGameOver;

            this.screen.Visible = !IsGameOver;
            this.textGameOver.Visible = IsGameOver;
            this.buttonStart.Visible = IsGameOver;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ChangeGameState();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            hubConnection.SendAsync("UpdateClients");
        }
        public Image Background
        {
            get
            {
                return Background_;
            }

            set
            {
                Background_ = value;
            }
        }

        private void Draw()
        {
            this.Background = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\World.jpg");
            if (Background != null)
            {
                imgGraph.DrawImage(Background, 0, 0, squareSize * numSquaresX, squareSize * numSquaresY);
            }
           
            var gridBrush = new SolidBrush(Color.LightGray);
            var gridPen = new Pen(gridBrush);

            for (int i = 1; i < numSquaresX; ++i)
                imgGraph.DrawLine(gridPen, 0, i * squareSize, squareSize * numSquaresX, i * squareSize);

            for (int i = 1; i < numSquaresX; ++i)
                imgGraph.DrawLine(gridPen, i * squareSize, 0, i * squareSize, squareSize * numSquaresY);

            if (world == null) return;

            //Draw
            Image playerColor = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\Player.png");
            Image rockColor = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\BlockDestructible.png");
            Image wallColor = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\BlockNonDestructible.png");
            Image bombColor = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\Bombe.png");
            Image firebombColor = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\Fire_Bomb.png");
            Image icebombColor = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\Ice_Bomb.png");
            Image fireColor = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\Fire.png");
            Image iceColor = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\BlockIce.png");

            Image itemColor = Image.FromFile(@"C:\Users\44421\Desktop\bomber2\BombermanClient\Sprites\BlockItem.png");

            for (int i = 0; i < world.GetLength(0); i++)
            {
                for (int j = 0; j < world[i].Length; j++)
                {

                    if (world[i][j].entity is Player)
                        world[i][j].entity.Draw(playerColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                   
                    else if (world[i][j].bomb is FireBomb)
                        world[i][j].bomb.Draw(firebombColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].bomb is IceBomb)
                        world[i][j].bomb.Draw(icebombColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].bomb != null)
                        world[i][j].bomb.Draw(bombColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);

                    else if (world[i][j].entity is Fire)
                        imgGraph.DrawImage(fireColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1);
                    else if (world[i][j].entity is IceWall)
                        imgGraph.DrawImage(iceColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1);
                    else if (world[i][j].entity is DestructableWall)
                        world[i][j].entity.Draw(rockColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                   
                    else if (world[i][j].entity is ItemDropWall)
                        world[i][j].entity.Draw(itemColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                  
                    else if (world[i][j].entity is IndestructableWall)
                        world[i][j].entity.Draw(wallColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                }
            }

            graph.DrawImage(img, 0, 0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
             if (hubConnection != null)
            {
                if (keyData == Keys.A || keyData == Keys.S || keyData == Keys.D || keyData == Keys.W)
                {
                    hubConnection.SendAsync("Movement", keyData.ToString());
                    return true;
                }
                else if (keyData == Keys.Space)
                {
                    hubConnection.SendAsync("PutDownBomb");
                    Task.Factory.StartNew(() => Thread.Sleep(4000))
                    .ContinueWith((t) =>
                    {
                        hubConnection.SendAsync("StateUpdate");

                    }, TaskScheduler.FromCurrentSynchronizationContext());
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async Task InitConnection()
        {
            hubConnection = new HubConnectionBuilder()
                            .WithUrl("https://localhost:5001/hub/")
                            .Build();
            hubConnection.On<string>("StateUpdate", StateUpdated);

            await hubConnection.StartAsync();
            buttonStart.Enabled = true;
        }

    }
}
