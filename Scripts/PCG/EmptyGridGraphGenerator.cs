﻿using System;
using UnityEngine;

namespace Chinchillada.Generation.Mazes
{
    [Serializable]
    public class EmptyGridGraphGenerator : GeneratorBase<GridGraph>
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
      
        [SerializeField] private bool connected;
        
        protected override GridGraph GenerateInternal()
        {
            return this.connected
                ? GridGraph.FullyConnected(this.width, this.height)
                : new GridGraph(this.width, this.height);
        }
    }
}