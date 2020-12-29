﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uWin
{
    public partial class uNotifyIconWrapper : Component
    {
        public uNotifyIconWrapper()
        {
            InitializeComponent();

            toolStripMenuItem_Exit.Click += toolStripMenuItem_Exit_Click;
        }

        public uNotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
