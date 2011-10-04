﻿using System.Windows.Media;
using Caliburn.Micro;

namespace lsight.Settings.LogFileDefinition
{
    internal class LogFileDefinitionViewModel : PropertyChangedBase, ILogFileDefinition
    {
        private string path;

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyOfPropertyChange(() => Path);
            }
        }

        private Color color;

        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                NotifyOfPropertyChange(() => Color);
            }
        }
    }
}