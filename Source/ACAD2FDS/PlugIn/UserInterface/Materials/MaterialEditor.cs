﻿namespace Fds2AcadPlugin.UserInterface.Materials
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;
    using Common.UI;
    using MaterialManager.BLL;

    public partial class MaterialEditor : FormBase
    {
        #region Constants

        private const string LabelControlPrefix = "lbl";
        private const string Colon = ":";
        private const string DefaulText = "null";
        private const string DoubleValidationError = "Please, enter correct double value.";
        private const string Int32ValidationError = "Please, enter correct integer value.";
        private const int displacementY = 30;
        private const int displacementX = 200;

        #endregion

        #region Fields

        private readonly Material material;

        private Point startLocation = new Point { X = 10, Y = 10 };

        #endregion

        #region Properties

        public Material Material
        {
            get
            {
                return material;
            }
        }

        #endregion

        #region Constructors

        public MaterialEditor()
        {
            InitializeComponent();
            material = new Material();
            material.CONDUCTIVITY_RAMP = new List<Ramp>();
            material.SPECIFIC_HEAT_RAMP = new List<Ramp>();
        }

        public MaterialEditor(Material material)
        {
            InitializeComponent();
            this.material = material;
        }

        #endregion

        #region Handling Form Events

        private void On_MaterialEditor_Load(object sender, EventArgs e)
        {
            var count = 0;

            foreach (var propertyInfo in material.GetType().GetProperties())
            {
                var label = GetLabelForProperty(propertyInfo, count);
                var inputControl = MapPropertyToControl(propertyInfo, count);

                Controls.Add(label);
                Controls.Add(inputControl);
                count++;
            }

            // if (count > 10)
            //     count = 10;

            Size = new Size(Size.Width, Size.Height + displacementY * count);
            MinimumSize = Size;
            MaximumSize = Size;
        }

        #endregion

        #region Internal Implementation

        private Control MapPropertyToControl(PropertyInfo propertyInfo, int order)
        {
            var control = new Control();

            #region String or Double or Int

            if (propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(double) || propertyInfo.PropertyType == typeof(int))
            {
                var text = propertyInfo.GetValue(material, null) != null ? propertyInfo.GetValue(material, null).ToString() : DefaulText;

                control = new TextBox
                {
                    Location = new Point(startLocation.X + displacementX, startLocation.Y + displacementY * order),
                    Size = new Size(120, 15),
                    TabIndex = order,
                    Text = text
                };

                control.TextChanged += On_textBox_TextChanged;

                if (propertyInfo.PropertyType == typeof(double))
                    control.Validating += On_doubleTextBox_Validating;

                if (propertyInfo.PropertyType == typeof(int))
                    control.Validating += On_intTextBox_Validating;
            }

            #endregion

            #region Enum

            if (propertyInfo.PropertyType.IsEnum)
            {
                control = CreateComboBox(order);

                var enumValues = Enum.GetValues(propertyInfo.PropertyType);

                foreach (var value in enumValues)
                {
                    (control as ComboBox).Items.Add(value);
                }

                (control as ComboBox).SelectedIndex = (int)propertyInfo.GetValue(material, null);
                (control as ComboBox).SelectedIndexChanged += On_comboBox_SelectedIndexChanged;
            }

            #endregion

            #region Bool

            if (propertyInfo.PropertyType == typeof(bool))
            {
                control = CreateComboBox(order);

                (control as ComboBox).Items.Add(true);
                (control as ComboBox).Items.Add(false);
                (control as ComboBox).SelectedItem = (bool)propertyInfo.GetValue(material, null);
                (control as ComboBox).SelectedIndexChanged += On_comboBox_SelectedIndexChanged;
            }

            #endregion

            #region List<Ramp>

            if (propertyInfo.PropertyType == typeof(List<Ramp>))
            {
                control = new Button
                {
                    Size = new Size(120, 20),
                    Location = new Point(startLocation.X + displacementX, startLocation.Y + displacementY * order - 3),
                    TabIndex = order,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Text = "Edit"
                };

                (control as Button).Click += On_rampCollectionEdit_Click;
                (control as Button).Tag = propertyInfo.GetValue(material, null);
            }

            #endregion

            control.Name = propertyInfo.Name;

            return control;
        }

        private Control GetLabelForProperty(PropertyInfo propertyInfo, int order)
        {
            return new Label
            {
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Location = new Point(startLocation.X, startLocation.Y + displacementY * order),
                AutoSize = true,
                Name = string.Concat(LabelControlPrefix, propertyInfo.Name),
                Text = string.Concat(propertyInfo.Name, Colon)
            };
        }

        private Control CreateComboBox(int order)
        {
            return new ComboBox
            {
                FormattingEnabled = true,
                Location = new Point(startLocation.X + displacementX, startLocation.Y + displacementY * order - 3),
                Name = "cbMaterialType",
                Size = new Size(120, 15),
                TabIndex = order,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private void SetProperyValue(string propertyName, object propertyValue)
        {
            var propertyInfo = typeof(Material).GetProperty(propertyName);

            object value = null;

            if (propertyInfo.PropertyType == typeof(string))
                value = propertyValue as string;

            if (propertyInfo.PropertyType == typeof(double))
                value = double.Parse(propertyValue.ToString());

            if (propertyInfo.PropertyType == typeof(int))
                value = int.Parse(propertyValue.ToString());

            if (value == null)
                value = propertyValue;

            propertyInfo.SetValue(material, value, null);
        }

        #endregion

        #region Handling Dynamic Control's Events

        private void On_textBox_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = ValidateChildren();

            if (btnSave.Enabled)
                SetProperyValue((sender as Control).Name, (sender as TextBox).Text);
        }

        private void On_doubleTextBox_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
            e.Cancel = false;

            double value;

            if (Double.TryParse((sender as TextBox).Text, out value))
            {
                errorProvider.SetError((Control)sender, string.Empty);
                e.Cancel = false;
            }
            else
            {
                errorProvider.SetError((Control)sender, DoubleValidationError);
                e.Cancel = true;
            }
        }

        private void On_intTextBox_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
            e.Cancel = false;

            int value;

            if (Int32.TryParse((sender as TextBox).Text, out value))
            {
                errorProvider.SetError((Control)sender, string.Empty);
                e.Cancel = false;
            }
            else
            {
                errorProvider.SetError((Control)sender, Int32ValidationError);
                e.Cancel = true;
            }
        }

        private void On_rampCollectionEdit_Click(object sender, EventArgs e)
        {
            var rampCollectionEditor = new EditRamp((List<Ramp>)(sender as Button).Tag);
            rampCollectionEditor.ShowDialog(this);
        }

        private void On_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (sender as ComboBox);

            if (comboBox == null)
                return;

            var propertyValue = comboBox.SelectedItem;

            SetProperyValue(comboBox.Name, propertyValue);
        }

        #endregion

        #region Handling Static Control's Events

        private void On_btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
