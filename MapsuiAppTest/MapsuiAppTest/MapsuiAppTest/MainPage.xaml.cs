using Mapsui.Geometries;
using Mapsui.Projection;
using Mapsui.UI;
using Mapsui.UI.Forms;
using Mapsui.UI.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace MapsuiAppTest
{
    /// <summary>
    /// This is a sample application to test the polyline for Mapsui
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        /// <summary>
        ///  Keep a reference to the mylocation layer, so i could use it later
        /// </summary>
        private MyLocationLayer _MyLocationLayer;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            InitMap();
            CreatePins();
            CreateDrawables();
        }

        /// <summary>
        /// Setup the map limiter to remove the white borders
        /// </summary>
        private void InitMap()
        {
            map.Map = new Mapsui.Map();
            map.Map.Layers.Insert(0, Mapsui.Utilities.OpenStreetMap.CreateTileLayer());

            // Limit the map to remove the white borders
            map.Map.Limiter = new ViewportLimiterKeepWithin
            {
                PanLimits = new BoundingBox(
                   SphericalMercator.FromLonLat(-180, 85),
                   SphericalMercator.FromLonLat(180, -85))
            };

            // Im not interested in the myLocation layer
            foreach (var l in map.Map.Layers)
            {
                if (l is MyLocationLayer)
                    _MyLocationLayer = l as MyLocationLayer;
            }
            map.Map.Layers.Remove(_MyLocationLayer);
        }

        /// <summary>
        /// Randomly create several pins
        /// </summary>
        private void CreatePins()
        {
            var positions = new List<Position>() { new Position(-6.280363, -57.856264), new Position(-1.335834, -111.461880), new Position(-1.335834, -145.899831), new Position(-9.761041, -181.567089), new Position(-29.366861, -212.937655), new Position(-25.133358, -231.776641) };

            var pins = new List<Pin>();
            foreach (var pos in positions)
            {
                pins.Add(
                    new Pin(map)
                    {
                        Label = $"Pin",
                        Address = $"Test",
                        Position = pos,
                        Type = PinType.Pin,
                        Color = Color.Red,
                        Scale = 50 / 100f
                    }
                );
            }
            foreach (var p in pins)
                map.Pins.Add(p);
        }

        /// <summary>
        /// Randomly create a polyline between the pins
        /// </summary>
        private void CreateDrawables()
        {
            var drawables = new List<Drawable>();

            Polyline line = new Polyline { StrokeWidth = 4, StrokeColor = Color.Blue };
            foreach (var p in map.Pins)
                line.Positions.Add(p.Position);
            drawables.Add(line);

            foreach (var d in drawables)
                map.Drawables.Add(d);

        }
    }
}
