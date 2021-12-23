using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;


/*
 * Разработать в WPF приложении класс WeatherControl, моделирующий погодную сводку 
 * – температуру (целое число в диапазоне от -50 до +50), направление ветра (строка), 
 * - скорость ветра (целое число), 
 * - наличие осадков (возможные значения: 0 – солнечно, 1 – облачно, 2 – дождь, 3 – снег. Можно использовать целочисленное значение, либо создать перечисление enum). 
 * Свойство «температура» преобразовать в свойство зависимости.
 */

namespace Lab06___WpfApp
{
    public class WeatherControl : DependencyObject
    {
        private int windSpeed;

        public enum precipitation
        {
            sunny = 0,
            cloudy,
            rain,
            snow
        }

        public static readonly DependencyProperty TemperatureProperty;
        public int WindSpeed
        {
            set
            {
                if (value >= 0)
                {
                    windSpeed = value;
                }
                else
                {
                    windSpeed = 0;
                }
            }
            get
            {
                return windSpeed;
            }
        }
        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }
        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    null,
                    new CoerceValueCallback(CoreTemperature),
                    true,
                    UpdateSourceTrigger.PropertyChanged // Поставил обновление элемента при его изменении, т.к. температура может изменяться в любой момент, а как следствие ее нужно отражать
                    ),
                new ValidateValueCallback(ValidateTemperature)
                );
        }

        private static bool ValidateTemperature(object value)
        {
            int v = (int)value;
            if ((v >= -50) && (v <= 50))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static object CoreTemperature(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if ((v >= -50) && (v <= 50))
            {
                return v;
            }
            else
            {
                return 0;
            }
        }
    }
}
