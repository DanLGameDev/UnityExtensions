using UnityEngine;

namespace DGP.UnityExtensions
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask layerMask, int layer) {
            return layerMask == (layerMask | (1 << layer));
        }
        
        public static LayerMask AddLayer(this LayerMask layerMask, int layer) {
            layerMask |= (1 << layer);
            return layerMask;
        }
        
        public static LayerMask RemoveLayer(this LayerMask layerMask, int layer) {
            layerMask &= ~(1 << layer);
            return layerMask;
        }
        
        public static LayerMask ToggleLayer(this LayerMask layerMask, int layer) {
            if (layerMask.Contains(layer)) {
                layerMask = layerMask.RemoveLayer(layer);
            } else {
                layerMask = layerMask.AddLayer(layer);
            }
            return layerMask;
        }
        
        public static int[] ToLayerArray(this LayerMask layerMask) {
            var layers = new System.Collections.Generic.List<int>();
            for (int i = 0; i < 32; i++) {
                if (layerMask.Contains(i)) {
                    layers.Add(i);
                }
            }
            return layers.ToArray();
        }
    }
}