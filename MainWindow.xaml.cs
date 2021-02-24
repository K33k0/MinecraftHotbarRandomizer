using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Gma.System.MouseKeyHook;

namespace MinecraftHotbarRandomizer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private IKeyboardMouseEvents m_GlobalHook;
        public List<int> Weights = new List<int>();
        Random rnd = new Random();
        InputSimulator Input = new InputSimulator();
        int keyDelay = 50;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (Weights.Count == 0)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                int r = rnd.Next(Weights.Count);
                var selection = Weights[r];
                switch (selection)
                {
                    case 1:
                        Input.Keyboard.KeyDown(VirtualKeyCode.VK_1);
                        Thread.Sleep(keyDelay);
                        Input.Keyboard.KeyUp(VirtualKeyCode.VK_1);
                        break;
                    case 2:
                        Input.Keyboard.KeyDown(VirtualKeyCode.VK_2);
                        Thread.Sleep(keyDelay);
                        Input.Keyboard.KeyUp(VirtualKeyCode.VK_2);
                        break;
                    case 3:
                        Input.Keyboard.KeyDown(VirtualKeyCode.VK_3);
                        Thread.Sleep(keyDelay);
                        Input.Keyboard.KeyUp(VirtualKeyCode.VK_3);
                        break;
                    case 4:
                        Input.Keyboard.KeyDown(VirtualKeyCode.VK_4);
                        Thread.Sleep(keyDelay);
                        Input.Keyboard.KeyUp(VirtualKeyCode.VK_4);
                        break;
                    case 5:
                        Input.Keyboard.KeyDown(VirtualKeyCode.VK_5);
                        Thread.Sleep(keyDelay);
                        Input.Keyboard.KeyUp(VirtualKeyCode.VK_5);
                        break;
                    case 6:
                        Input.Keyboard.KeyDown(VirtualKeyCode.VK_6);
                        Thread.Sleep(keyDelay);
                        Input.Keyboard.KeyUp(VirtualKeyCode.VK_6);
                        break;
                    case 7:
                        Input.Keyboard.KeyDown(VirtualKeyCode.VK_7);
                        Thread.Sleep(keyDelay);
                        Input.Keyboard.KeyUp(VirtualKeyCode.VK_7);
                        break;
                    case 8:
                        Input.Keyboard.KeyDown(VirtualKeyCode.VK_8);
                        Thread.Sleep(keyDelay);
                        Input.Keyboard.KeyUp(VirtualKeyCode.VK_8);
                        break;
                    case 9:
                        Input.Keyboard.KeyDown(VirtualKeyCode.VK_9);
                        Thread.Sleep(keyDelay);
                        Input.Keyboard.KeyUp(VirtualKeyCode.VK_9);
                        break;
                }
            }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;
            
            m_GlobalHook.Dispose();
        }


        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Weights = new List<int>();
            var stackpanels = root.Children.OfType<StackPanel>();
            var sliders = new List<Slider>();
            foreach (var panel in stackpanels)
            {
                if (panel.Children.OfType<Slider>().FirstOrDefault() != null)
                {
                    sliders.Add(panel.Children.OfType<Slider>().FirstOrDefault());

                }
            }


            foreach (var s in sliders)
            {
                var keyNumber = int.Parse(s.Name.Replace("Slider", ""));

                for (int i = 0; i < s.Value; i++)
                {
                    Weights.Add(keyNumber);
                }
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void PowerSwitch_OnClick(object sender, RoutedEventArgs e)
        {
            if (PowerSwitch.IsChecked == true)
            {
                Subscribe();
                PowerSwitch.Content = "Turn Off";
            }
            else
            {
                Unsubscribe();
                PowerSwitch.Content = "Turn On";

            }
        }

        private void Delay_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (delay.Value != null) keyDelay = (int)delay.Value;
        }
    }
}
