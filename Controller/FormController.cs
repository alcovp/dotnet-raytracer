using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataStructure;
using SceneDefinition;
using System.Threading;
using System.Drawing.Imaging;

namespace Controller
{
    public partial class FormController : Form
    {
        private int xResolution;
        private int yResolution;
        private int xResolutionFast;
        private int yResolutionFast;
        private Thread superThread;
        private XYZ eye_p = new XYZ { X = 0, Y = 0, Z = -9.9 }; // point
        private XYZ view_v = new XYZ { X = 0, Y = 0, Z = 1 }.Normalize(); // unit-vector
        private XYZ up_v = new XYZ { X = 0, Y = 1, Z = 0 }.Normalize(); // unit-vector
        private bool realTimeOn = false;
        private bool cameraMoveForward = false;
        private bool cameraMoveBackward = false;
        private bool cameraMoveLeft = false;
        private bool cameraMoveRight = false;
        private bool cameraMoveDown = false;
        private bool cameraMoveUp = false;
        private float cameraSpeed = 0.1f;
        private float cameraRotationSpeed = 1;
        private Point mouseDefaultLocation = new Point(500, 500);
        private int mouseDeltaX = 0;
        private int mouseDeltaY = 0;
        private DateTime lastTimerTick = DateTime.Now;
        private FormOutput outputForm = null;

        public FormController()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            timer1.Start();
        }

        private void Solve_button_Click(object sender, EventArgs e)
        {
            outputForm = new FormOutput();
            outputForm.Show();

            realTimeStop();
            superThread = new Thread(() => SuperMethod(false));
            superThread.Start();
            toolStripProgressBar1.Visible = true;
        }

        private void realTimeBtn_Click(object sender, EventArgs e)
        {
            if (!realTimeOn)
            {
                Cursor.Hide();
                Cursor.Position = mouseDefaultLocation;
                realTimeOn = true;
            }
            if (superThread != null && superThread.IsAlive)
            {
                superThread.Abort();
            }
            superThread = new Thread(() => SuperMethod(true));
            superThread.Start();
            toolStripProgressBar1.Visible = false;
        }

        private void realTimeStop()
        {
            if (realTimeOn)
            {
                Cursor.Show();
                realTimeOn = false;
            }
            if (superThread != null && superThread.IsAlive)
            {
                superThread.Abort();
            }
        }

        private void SuperMethod(bool realTime)
        {
            var startTime = DateTime.Now;

            #region Texture loading
            //Bitmap sphereTexture = (Bitmap)Bitmap.FromFile(@"D:\workspace\C#\RayTracingMethod\earth.jpg");
            #endregion

            #region Scene objects definition
            Scene.NewScene();
            //Scene.AddBoundingBox(0, 10, 0, 10, 0, 10, new Material { Color = Color.FromArgb(100, 100, 255) });
            // back
            Scene.AddPlane(new XYZ { X = 0, Y = 0, Z = -1 }, 5, new Material { Color = Color.FromArgb(190, 190, 190), Reflectivity = 0 });
            // face
            Scene.AddPlane(new XYZ { X = 0, Y = 0, Z = 1 }, 10, new Material { Color = Color.FromArgb(190, 190, 190), Reflectivity = 0 });
            // bottom
            Scene.AddPlane(new XYZ { X = 0, Y = 1, Z = 0 }, 5, new Material { Color = Color.FromArgb(190, 190, 190), Reflectivity = 0 });
            // top
            Scene.AddPlane(new XYZ { X = 0, Y = -1, Z = 0 }, 5, new Material { Color = Color.FromArgb(190, 190, 190), Reflectivity = 0 });
            // left
            Scene.AddPlane(new XYZ { X = 1, Y = 0, Z = 0 }, 5, new Material { Color = Color.FromArgb(190, 190, 190), Reflectivity = 0 });
            // right
            Scene.AddPlane(new XYZ { X = -1, Y = 0, Z = 0 }, 5, new Material { Color = Color.FromArgb(190, 190, 190), Reflectivity = 0 });

            //Scene.AddSphere(new XYZ { X = 5, Y = 0, Z = 10 }, 5, new Material { Color = Color.FromArgb(0, 255, 0), Reflectivity = 0 });
            //Scene.AddSphere(new XYZ { X = 8, Y = 2, Z = 7 }, 2, new Material { Color = Color.FromArgb(255, 255, 0), Reflectivity = 0 });
            //Scene.AddSphere(new XYZ { X = 4, Y = 6, Z = 4 }, 0.5, new Material { Color = Color.FromArgb(0, 255, 255), Reflectivity = 0 });
            //Scene.AddSphere(new XYZ { X = 8, Y = 6.5, Z = 6 }, 0.5, new Material { Color = Color.FromArgb(255, 100, 100), Reflectivity = 0 });
            //Scene.AddSphere(new XYZ { X = 7, Y = 7.5, Z = 7 }, 0.5, new Material { Color = Color.FromArgb(255, 100, 100), Reflectivity = 0 });
            //Scene.AddSphere(new XYZ { X = 6, Y = 8.5, Z = 8 }, 0.5, new Material { Color = Color.FromArgb(255, 100, 100), Reflectivity = 0 });
            //Scene.AddSphere(new XYZ { X = 5, Y = 9.5, Z = 9 }, 0.5, new Material { Color = Color.FromArgb(255, 100, 100), Reflectivity = 0 });
            double[][] quadricFormMatrix = new double[][]
            {
                new double[] { 1, 0, 0 },
                new double[] { 0, 1, 0 },
                new double[] { 0, 0, 1 }
            };
            double[] linearFormVector = new double[] { 0, 0, 0 };
            double absoluteTerm = -10;
            //Scene.AddQuadricSurface(quadricFormMatrix, linearFormVector, absoluteTerm, new Material { Color = Color.FromArgb(255, 255, 255), Reflectivity = 1, Refractivity = 0.7, RefractiveIndex = 1.52 });
            double[][] quadricFormMatrix2 = new double[][]
            {
                new double[] { 1, 0, 0 },
                new double[] { 0, 0, 0 },
                new double[] { 0, 0, 1 } 
            };
            double[] linearFormVector2 = new double[] { 0, 0, 0 };
            double absoluteTerm2 = -1.6;
            //Scene.AddQuadricSurface(quadricFormMatrix2, linearFormVector2, absoluteTerm2, new Material { Color = Color.FromArgb(0, 0, 0), Reflectivity = 0, Refractivity = 0.9, RefractiveIndex = 1.52 });

            //Scene.AddSphere(new XYZ { X = -2.5, Y = 0, Z = -10 }, 2, new Material { Color = Color.FromArgb(0, 205, 255), Reflectivity = 0 });
            //Scene.AddPlain(new XYZ { X = 0, Y = 1, Z = 0 }, 3, new Material { Color = Color.FromArgb(0, 100, 200), Refractivity = 0.8, RefractiveIndex = 1.33 });
            //Scene.AddSphere2(new XYZ { X = 0, Y = -3, Z = 0 }, 2, new Material { Color = Color.FromArgb(255, 255, 0), Reflectivity = 0.6, Refractivity = 0, RefractiveIndex = 1.52 });
            Scene.AddSphere2(new XYZ { X = 3.5, Y = 2, Z = 3.5 }, 1.5, new Material { Color = Color.FromArgb(12, 173, 43), Reflectivity = 0.8, Refractivity = 0, RefractiveIndex = 1.52 });
            Scene.AddSphere2(new XYZ { X = 0, Y = 0, Z = 0 }, 2, new Material { Color = Color.FromArgb(255, 255, 255), Reflectivity = 0, Refractivity = 0.5, RefractiveIndex = 1.52 });
            //Scene.AddSphere2(new XYZ { X = 2, Y = 0, Z = 5 }, 1, new Material { Color = Color.FromArgb(0, 255, 0), Reflectivity = 0, Refractivity = 0.7, RefractiveIndex = 1.52 });
            //Scene.AddSphere2(new XYZ { X = 0, Y = 0, Z = 0 }, 3, new Material { Color = Color.FromArgb(255, 255, 255), Reflectivity = 1, Refractivity = 0.7, RefractiveIndex = 1.52 });

            // primary light
            Scene.AddLight(new XYZ { X = 0, Y = 4, Z = 0 }, 20, Color.FromArgb(255, 255, 255), 0.2);
            // ambient light
            //Scene.AddLight(new XYZ { X = -4.9, Y = 4.9, Z = -4.9 }, 0, Color.FromArgb(255, 255, 255), 0.2);
            // secondary lights
            //Scene.AddLight(new XYZ { X = -4.9, Y = -4.9, Z = 4.9 }, 9.5, Color.FromArgb(255, 255, 255), 0);
            #endregion

            #region Camera definition
            //eye_p = new XYZ { X = 0, Y = 0, Z = -9.9 }; // point
            //view_v = new XYZ { X = 0, Y = 0, Z = 1 }.Normalize(); // unit-vector
            //up_v = new XYZ { X = 0, Y = 1, Z = 0 }.Normalize(); // unit-vector

            //XYZ eye_p = new XYZ { X = 4.9, Y = 4.9, Z = 0 }; // point
            //XYZ view_v = new XYZ { X = -1, Y = -1, Z = 0 }.Normalize(); // unit-vector
            //XYZ up_v = new XYZ { X = 0, Y = 1, Z = 0 }.Normalize(); // unit-vector

            //XYZ eye_p = new XYZ { X = 0, Y = 4.9, Z = -4.9 }; // point
            //XYZ view_v = new XYZ { X = 0, Y = -1, Z = 1 }.Normalize(); // unit-vector
            //XYZ up_v = new XYZ { X = 0, Y = 1, Z = 0 }.Normalize(); // unit-vector
            #endregion

            xResolution = int.Parse(xResolution_textBox.Text);
            yResolution = int.Parse(yResolution_textBox.Text);
            xResolutionFast = int.Parse(xResolutionFast_textBox.Text);
            yResolutionFast = int.Parse(yResolutionFast_textBox.Text);
            int pixelSize = int.Parse(textBoxPixelSize.Text);

            int antialiasing = 0;
            int recursion = int.Parse(recursion_textBox.Text);

            if (comboBox1.InvokeRequired)
            {
                comboBox1.Invoke(new MethodInvoker(delegate { antialiasing = (comboBox1.SelectedItem == null ? 0 : int.Parse(comboBox1.SelectedItem.ToString())); }));
            }
            else
            {
                antialiasing = (comboBox1.SelectedItem == null ? 0 : int.Parse(comboBox1.SelectedItem.ToString()));
            }

            // paint
            //if (pictureBox1.InvokeRequired)
            //{
            //    pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.Size = new Size(xResolution, yResolution); }));
            //}
            //else
            //{
            //    pictureBox1.Size = new Size(xResolution, yResolution);
            //}

            Bitmap bitmap = null;
            if (realTime)
            {
                bitmap = new Bitmap(xResolutionFast * pixelSize, yResolutionFast * pixelSize);
            }
            else
            {
                bitmap = new Bitmap(xResolution, yResolution);
            }

            while (true)
            {
                if (realTime)
                {
                    startTime = DateTime.Now;
                    Canvas.CreateFrame(xResolutionFast, yResolutionFast, 90, eye_p, view_v, up_v, antialiasing, recursion);
                }
                else
                {
                    Canvas.CreateFrame(xResolution, yResolution, 90, eye_p, view_v, up_v, antialiasing, recursion);
                }
                Pixel[][] pixels = Canvas.GetComputedPixels();
                if (pixels != null)
                {
                    if (realTime)
                    {
                        for (int i = 0; i < pixels.Length; i++)
                        {
                            for (int j = 0; j < pixels[0].Length; j++)
                            {
                                for (int k = 0; k < pixelSize; k++)
                                {
                                    for (int l = 0; l < pixelSize; l++)
                                    {
                                        bitmap.SetPixel(pixels[i][j].X * pixelSize + k, pixels[i][j].Y * pixelSize + l, pixels[i][j].Color);

                                    }
                                }
                            }
                        }
                        SetFrame(bitmap.Clone() as Bitmap, xResolutionFast * pixelSize, yResolutionFast * pixelSize);
                    }
                    else
                    {
                        for (int i = 0; i < pixels.Length; i++)
                        {
                            for (int j = 0; j < pixels[0].Length; j++)
                            {
                                bitmap.SetPixel(pixels[i][j].X, pixels[i][j].Y, pixels[i][j].Color);
                            }
                        }
                        outputForm.SetFrame(bitmap, xResolution, yResolution);
                    }
                }

                var elapsedTime = DateTime.Now - startTime;
                if (statusStrip1.InvokeRequired)
                {
                    statusStrip1.Invoke(new MethodInvoker(delegate { toolStripStatusLabelBuildTime.Text = elapsedTime.ToString(); }));
                }
                else
                {
                    toolStripStatusLabelBuildTime.Text = elapsedTime.ToString();
                }

                if (!realTime)
                {
                    break;
                }
            }
        }


        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (superThread != null)
            {
                superThread.Abort();
            }
        }

        public void SetFrame(Bitmap image, int xResolution, int yResolution)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new MethodInvoker(delegate
                {
                    pictureBox1.Size = new Size(xResolution, yResolution);
                    pictureBox1.Image = image;
                }));
            }
            else
            {
                pictureBox1.Size = new Size(xResolution, yResolution);
                pictureBox1.Image = image;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    cameraMoveLeft = true;
                    break;
                case Keys.D:
                    cameraMoveRight = true;
                    break;
                case Keys.S:
                    cameraMoveBackward = true;
                    break;
                case Keys.W:
                    cameraMoveForward = true;
                    break;
                case Keys.ShiftKey:
                    cameraMoveDown = true;
                    break;
                case Keys.Space:
                    cameraMoveUp = true;
                    break;
                case Keys.Escape:
                    realTimeStop();
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    cameraMoveLeft = false;
                    break;
                case Keys.D:
                    cameraMoveRight = false;
                    break;
                case Keys.S:
                    cameraMoveBackward = false;
                    break;
                case Keys.W:
                    cameraMoveForward = false;
                    break;
                case Keys.ShiftKey:
                    cameraMoveDown = false;
                    break;
                case Keys.Space:
                    cameraMoveUp = false;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var newTick = DateTime.Now;
            var time = newTick - lastTimerTick;
            if (realTimeOn)
            {
                var left_v = view_v.OuterProduct(up_v).Normalize();
                if (cameraMoveForward)
                {
                    eye_p = eye_p.Add(view_v.Product(cameraSpeed)); // TODO умножать на промежуток времени
                }
                if (cameraMoveBackward)
                {
                    eye_p = eye_p.Substract(view_v.Product(cameraSpeed));
                }
                if (cameraMoveLeft)
                {
                    eye_p = eye_p.Add(left_v.Product(cameraSpeed));
                }
                if (cameraMoveRight)
                {
                    eye_p = eye_p.Substract(left_v.Product(cameraSpeed));
                }
                if (cameraMoveUp)
                {
                    eye_p = eye_p.Add(up_v.Product(cameraSpeed));
                }
                if (cameraMoveDown)
                {
                    eye_p = eye_p.Substract(up_v.Product(cameraSpeed));
                }

                mouseDeltaX = Cursor.Position.X - mouseDefaultLocation.X;
                mouseDeltaY = Cursor.Position.Y - mouseDefaultLocation.Y;
                Cursor.Position = mouseDefaultLocation;

                if (mouseDeltaY != 0)
                {
                    view_v = view_v.RotateFromAxis((float)-mouseDeltaY / 1000, left_v);
                    up_v = up_v.RotateFromAxis((float)-mouseDeltaY / 1000, left_v);
                }
                if (mouseDeltaX != 0)
                {
                    view_v = view_v.RotateFromAxis((float)mouseDeltaX / 1000, new XYZ() { X = 0, Y = 1, Z = 0});
                    up_v = up_v.RotateFromAxis((float)mouseDeltaX / 1000, new XYZ() { X = 0, Y = 1, Z = 0 });
                }

                toolStripStatusLabelCoords.Text = "x:" + eye_p.X.ToString("F1") + " y:" + eye_p.Y.ToString("F1") + " z:" + eye_p.Z.ToString("F1");
            }
            if (toolStripProgressBar1.Visible)
            {
                if (Canvas.allPixelsCount != 0)
                {
                    if (Canvas.donePixelsCount > 0)
                    {
                        var value = (int)((float)Canvas.donePixelsCount / (float)Canvas.allPixelsCount * 100);
                        if (value >= 99)
                        {
                            value = 100;
                        }
                        toolStripProgressBar1.Value = value;
                    }
                }
                else
                {
                    toolStripProgressBar1.Value = 100;
                }
            }
            lastTimerTick = newTick;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void FormController_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
