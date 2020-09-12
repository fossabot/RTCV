﻿namespace RTCV.UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using RTCV.CorruptCore;
    using RTCV.Common;

    public partial class NoteEditorForm : Form
    {
        private readonly INote _note;

        private readonly List<DataGridViewCell> _cells;

        private NoteEditorForm(INote note)
        {
            KeyDown += OnKeyDown;
            _note = note;
            InitializeComponent();
        }

        public NoteEditorForm(INote note, DataGridViewCell _cell) : this(note)
        {
            _cells = new List<DataGridViewCell>
            {
                _cell
            };
        }

        public NoteEditorForm(INote note, List<DataGridViewCell> _cells) : this(note)
        {
            this._cells = _cells;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            if (_note.Note != null)
            {
                tbNote.Text = _note.Note.Replace("\n", Environment.NewLine);
            }

            // Set window location
            if (UICore.NoteBoxPosition != new Point(0, 0))
            {
                this.Location = UICore.NoteBoxPosition;
            }
            if (UICore.NoteBoxSize != new Size(0, 0))
            {
                this.Size = UICore.NoteBoxSize;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.S) ||
                (e.KeyCode == Keys.Escape))
            {
                this.Close();
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            UICore.NoteBoxSize = this.Size;
            UICore.NoteBoxPosition = this.Location;

            var cleanText = string.Join("\n", tbNote.Lines.Select(it => it.Trim()));

            if (cleanText == "[DIFFERENT]")
            {
                return;
            }

            var oldText = _note.Note;

            if (string.IsNullOrEmpty(cleanText))
            {
                _note.Note = string.Empty;
                if (_cells != null)
                {
                    foreach (DataGridViewCell cell in _cells)
                    {
                        cell.Value = string.Empty;
                    }
                }
            }
            else
            {
                _note.Note = cleanText;
                if (_cells != null)
                {
                    foreach (DataGridViewCell cell in _cells)
                    {
                        cell.Value = "📝";
                    }
                }
            }

            //If our cell comes from the GH's dgv and the text changed, prompt unsavededits
            if (oldText != cleanText && _cells?.First()
                ?.DataGridView == S.GET<StockpileManagerForm>()
                .dgvStockpile)
            {
                S.GET<StockpileManagerForm>().UnsavedEdits = true;
            }
        }

        private void OnFormShown(object sender, EventArgs e)
        {
            tbNote.DeselectAll();
        }
    }
}
