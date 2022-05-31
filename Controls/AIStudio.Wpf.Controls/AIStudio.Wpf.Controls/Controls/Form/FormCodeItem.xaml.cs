﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using AIStudio.Wpf.Controls.Behaviors;
using AIStudio.Wpf.Controls.Bindings;
using AIStudio.Wpf.Controls.Converter;

namespace AIStudio.Wpf.Controls
{
    [TemplatePart(Name = PART_Header, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = PART_ContentPresenter, Type = typeof(ContentPresenter))]
    public class FormCodeItem : FormItem
    {
        #region Constants
        private const string PART_Header = "PART_Header";
        private const string PART_ContentPresenter = "PART_ContentPresenter";
        #endregion Constants

        private ContentPresenter _header;
        private ContentPresenter _contentPresenter;
        private Control _control;


        #region ControlType
        public static readonly DependencyProperty ControlTypeProperty = DependencyProperty.Register(
            "ControlType", typeof(FormControlType), typeof(FormCodeItem), new PropertyMetadata(FormControlType.TextBox, OnControlTypeChanged));

        private static void OnControlTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FormCodeItem formCodeItem)
            {
                formCodeItem.GetControl((FormControlType)e.NewValue);
            }
        }

        public FormControlType ControlType
        {
            get
            {
                return (FormControlType)GetValue(ControlTypeProperty);
            }
            set
            {
                SetValue(ControlTypeProperty, value);
            }
        }

        #endregion

        #region Path
        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
            "Path", typeof(string), typeof(FormCodeItem), new PropertyMetadata("", OnPathChanged));

        private static void OnPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FormCodeItem formCodeItem)
            {
                formCodeItem.GetControl(formCodeItem.ControlType);
            }
        }

        [Browsable(true)]
        public string Path
        {
            get
            {
                return (string)GetValue(PathProperty);
            }
            set
            {
                SetValue(PathProperty, value);
            }
        }
        #endregion

        #region ItemsSource
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(string), typeof(FormCodeItem), new PropertyMetadata(null, OnItemsSourceChanged));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FormCodeItem formCodeItem)
            {
                formCodeItem.SetItemsSource((string)e.NewValue);
            }
        }

        [Browsable(true)]
        public string ItemsSource
        {
            get
            {
                return (string)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        #endregion

        static FormCodeItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormCodeItem), new FrameworkPropertyMetadata(typeof(FormCodeItem), FrameworkPropertyMetadataOptions.Inherits));
        }


        private void GetControl(FormControlType controlType)
        {
            if (_contentPresenter == null)
                return;

            bool hideHeader = false;
            switch (controlType)
            {
                case FormControlType.TextBox:
                    {
                        _control = new TextBox();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(TextBox.TextProperty, binding);
                        }
                        break;
                    }
                case FormControlType.ComboBox:
                    {
                        _control = new ComboBox();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(ComboBox.SelectedValueProperty, binding);
                            _control.SetValue(ComboBox.DisplayMemberPathProperty, "Text");
                            _control.SetValue(ComboBox.SelectedValuePathProperty, "Value");
                        }
                        break;
                    }
                case FormControlType.PasswordBox:
                    {
                        _control = new PasswordBox();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(PasswordBoxBindingBehavior.PasswordProperty, binding);
                        }
                        break;
                    }
                case FormControlType.DatePicker:
                    {
                        _control = new DatePicker();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(DatePicker.SelectedDateProperty, binding);
                        }
                        break;
                    }
                case FormControlType.TreeSelect:
                    {
                        _control = new TreeSelect();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.Mode = BindingMode.TwoWay;
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(TreeSelect.SelectedValueProperty, binding);
                            _control.SetValue(TreeSelect.DisplayMemberPathProperty, "Text");
                            _control.SetValue(TreeSelect.SelectedValuePathProperty, "Value");
                        }
                        var dataTemplate = new HierarchicalDataTemplate();
                        dataTemplate.ItemsSource = new Binding("Children");
                        FrameworkElementFactory fef = new FrameworkElementFactory(typeof(TextBlock));
                        Binding bind = new Binding("Text");
                        fef.SetBinding(TextBlock.TextProperty, bind);
                        dataTemplate.VisualTree = fef;
                        (_control as TreeSelect).ItemTemplate = dataTemplate;
                        break;
                    }
                case FormControlType.MultiComboBox:
                    {
                        _control = new MultiComboBox();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.Mode = BindingMode.TwoWay;
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(CustomeSelectionValues.SelectedValuesProperty, binding);
                            _control.SetValue(ComboBox.DisplayMemberPathProperty, "Text");
                            _control.SetValue(ComboBox.SelectedValuePathProperty, "Value");
                        }
                        break;
                    }
                case FormControlType.IntegerUpDown:
                    {
                        _control = new IntegerUpDown();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(IntegerUpDown.ValueProperty, binding);
                        }
                        break;
                    }
                case FormControlType.LongUpDown:
                    {
                        _control = new LongUpDown();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(IntegerUpDown.ValueProperty, binding);
                        }
                        break;
                    }
                case FormControlType.DoubleUpDown:
                    {
                        _control = new DoubleUpDown();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(IntegerUpDown.ValueProperty, binding);
                        }
                        break;
                    }
                case FormControlType.DecimalUpDown:
                    {
                        _control = new DecimalUpDown();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(IntegerUpDown.ValueProperty, binding);
                        }
                        break;
                    }
                case FormControlType.DateTimeUpDown:
                    {
                        _control = new DateTimeUpDown();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(IntegerUpDown.ValueProperty, binding);
                        }
                        break;
                    }
                case FormControlType.CheckBox:
                    {
                        _control = new CheckBox();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(CheckBox.IsCheckedProperty, binding);
                        }
                        break;
                    }
                case FormControlType.ToggleButton:
                    {
                        _control = new ToggleButton();
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Binding binding = new Binding(Path);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            binding.ValidatesOnExceptions = true;
                            binding.ValidatesOnDataErrors = true;
                            binding.NotifyOnValidationError = true;
                            _control.SetBinding(ToggleButton.IsCheckedProperty, binding);
                        }
                        break;
                    }
                case FormControlType.Query:
                case FormControlType.Submit:
                    {
                        hideHeader = true;

                        _control = new Button();
                        Binding binding = new Binding($"DataContext.{Path}");
                        binding.RelativeSource = new RelativeSource { AncestorType = typeof(UserControl), Mode = RelativeSourceMode.FindAncestor };
                        _control.SetBinding(Button.CommandProperty, binding);
                        Binding binding2 = new Binding(".");
                        _control.SetBinding(Button.CommandParameterProperty, binding2);
                        Binding binding3 = new Binding("Header");
                        binding3.Source = this;
                        _control.SetBinding(Button.ContentProperty, binding3);
                        break;
                    }
                default:
                    {
                        _control = null;
                        break;
                    }
            }

            if (_header != null)
            {
                _header.Visibility = hideHeader ? Visibility.Collapsed : Visibility.Visible;
            }

            if (_control != null)
            {
                _control.SetCurrentValue(ControlAttach.ClearTextButtonProperty, true);
                _contentPresenter.Content = _control;
            }
        }

        private void SetItemsSource(string itemsSource)
        {
            if (_control != null)
            {
                var binding = new Binding($"DataContext.{itemsSource}");
                binding.RelativeSource = new RelativeSource { AncestorType = typeof(UserControl), Mode = RelativeSourceMode.FindAncestor };
                if (_control is ComboBox comboBox)
                {
                    comboBox.SetBinding(ComboBox.ItemsSourceProperty, binding);
                }
                else if (_control is TreeSelect treeSelect)
                {
                    treeSelect.SetBinding(TreeSelect.ItemsSourceProperty, binding);
                }
                else if (_control is MultiComboBox multiComboBox)
                {
                    multiComboBox.SetBinding(MultiComboBox.ItemsSourceProperty, binding);
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentPresenter = GetTemplateChild(PART_ContentPresenter) as ContentPresenter;
            _header = GetTemplateChild(PART_Header) as ContentPresenter;

            GetControl(ControlType);
            SetItemsSource(ItemsSource);
        }

        private string GetPath()
        {
            return string.IsNullOrEmpty(Path) ? "." : Path;
        }

        public override string ToString()
        {
            switch (ControlType)
            {
                case FormControlType.TextBox:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <TextBox Text=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.TextBox.Underline}}\"></TextBox>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.ComboBox:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <ComboBox SelectedValue=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ItemsSource=\"{{ac:ControlBinding {ItemsSource}}}\"  DisplayMemberPath=\"Text\" SelectedValuePath=\"Value\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.ComboBox.Underline}}\"></ComboBox>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.PasswordBox:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <PasswordBox ac:PasswordBoxBindingBehavior.Password=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.PasswordBox.Underline}}\"></PasswordBox>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.DatePicker:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <DatePicker SelectedDate=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.DatePicker.Underline}}\"></DatePicker>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.TreeSelect:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <ac:TreeSelect SelectedValue=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ItemsSource=\"{{ac:ControlBinding {ItemsSource}}}\"  DisplayMemberPath=\"Text\" SelectedValuePath=\"Value\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.TreeSelect.Underline}}\">" + "\r\n" +
$"              <ac:TreeSelect.ItemTemplate>" + "\r\n" +
$"                  <HierarchicalDataTemplate ItemsSource = \"{{Binding Children}}\">" + "\r\n" +
$"                      <StackPanel Orientation = \"Horizontal\">" + "\r\n" +
$"                          <TextBlock Text = \"{{Binding Text}}\" VerticalAlignment = \"Center\" />" + "\r\n" +
$"                      </StackPanel>" + "\r\n" +
$"                  </HierarchicalDataTemplate>" + "\r\n" +
$"              </ac:TreeSelect.ItemTemplate>" + "\r\n" +
$"          </ac:TreeSelect>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.MultiComboBox:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <ac:MultiComboBox ac:CustomeSelectionValues.SelectedValues=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ItemsSource=\"{{ac:ControlBinding {ItemsSource}}}\"  DisplayMemberPath=\"Text\" SelectedValuePath=\"Value\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.MultiComboBox.Underline}}\"></ac:MultiComboBox>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.IntegerUpDown:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <ac:IntegerUpDown Value=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.NumericUpDown}}\"></ac:IntegerUpDown>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.LongUpDown:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <ac:LongUpDown Value=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.NumericUpDown}}\"></ac:LongUpDown>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.DoubleUpDown:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <ac:DoubleUpDown Value=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.NumericUpDown}}\"></ac:DoubleUpDown>" + "\r\n" +
"        </ac:FormItem>";
                        return str;
                    }
                case FormControlType.DecimalUpDown:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <ac:DecimalUpDown Value=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.NumericUpDown}}\"></ac:DecimalUpDown>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.CheckBox:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <CheckBox IsChecked=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.CheckBox}}\"></CheckBox>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.ToggleButton:
                    {
                        string str =
$"      <ac:FormItem Header=\"{Header}\">" + "\r\n" +
$"          <ToggleButton IsChecked=\"{{Binding {GetPath()},Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}}\" ac:ControlAttach.ClearTextButton=\"True\" Style=\"{{DynamicResource AIStudio.Styles.ToggleButton.Switch}}\"></ToggleButton>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                case FormControlType.Query:
                case FormControlType.Submit:
                    {
                        string str =
$"      <ac:FormItem>" + "\r\n" +
$"          <Button Content=\"{Header}\" Command=\"{{ac:ControlBinding {GetPath()}}}\" CommandParameter=\"{{ Binding.}}\" Style=\"{{DynamicResource AIStudio.Styles.Button}}\"></Button>" + "\r\n" +
"       </ac:FormItem>";
                        return str;
                    }
                default: return base.ToString();
            }
        }

    }
}