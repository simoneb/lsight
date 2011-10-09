﻿using System;
using System.Windows.Media;
using Caliburn.Micro;

namespace lsight.Logs.Lines
{
    internal class LogLineViewModel : PropertyChangedBase
    {
        private string contents;

        public string Contents
        {
            get { return contents; }
            set
            {
                contents = value;
                NotifyOfPropertyChange(() => Contents);
            }
        }

        public string Path { get; set; }
        public DateTime Timestamp { get; set; }

        private SolidColorBrush brush;

        public SolidColorBrush Brush
        {
            get { return brush; }
            set
            {
                brush = value;
                NotifyOfPropertyChange(() => Brush);
            }
        }

        public LogLineViewModel(string contents, string path, Color color, DateTime timestamp)
        {
            Contents = contents;
            Path = path;
            Timestamp = timestamp;
            Brush = new SolidColorBrush(color);
        }

        public void ChangeColor(Color color)
        {
            Brush = new SolidColorBrush(color);
        }
    }
}