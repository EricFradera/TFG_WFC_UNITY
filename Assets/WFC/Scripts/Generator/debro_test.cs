using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;
using Unity.VisualScripting;
using UnityEngine;
using Resolution = UnityEngine.Resolution;

namespace WFC
{
    [ExecuteInEditMode]
    public class debro_test
    {
        // Start is called before the first frame update
        private Generate_Adjacency _adjacencyGen;
        private List<WFC2DTile> tileData;

        private Direction[] direction =
        {
            Direction.YPlus,
            Direction.XPlus,
            Direction.YMinus,
            Direction.XMinus
        };

        public debro_test(List<WFC2DTile> tileData)
        {
            this.tileData = tileData;
            _adjacencyGen = new Generate_Adjacency(this.tileData);
        }

        public ITopoArray<int> runWFC(int size)
        {
            // Define some sample data
            ITopoArray<int> sample = TopoArray.Create(new[]
            {
                new[] { 4, 3, 4, 3, 3 },
                new[] { 3, 0, 1, 2, 3 },
                new[] { 3, 3, 4, 3, 4 },
                new[] { 4, 3, 4, 3, 4 },
                new[] { 2, 0, 1, 2, 0 },
                new[] { 3, 0, 1, 2, 3 },
            }, periodic: false);

// Specify the model used for generation
            //OVERLAPPING MODEL
            //var model = buildCircuit();
            //HARDCODED ADJENCY
            //var model=new AdjacentModel(sample.ToTiles());
            //AUTOMATIC ADJENCY
            var model = generateMountain();

// Set the output dimensions
            var topology = new GridTopology(size, size, periodic: false);
// Acturally run the algorithm
            var propagator = new TilePropagator(model, topology);
            var status = propagator.Run();
            if (status != DeBroglie.Resolution.Decided) throw new Exception("Undecided");
            var output = propagator.ToValueArray<int>();

// Display the results
            return output;
        }

        private AdjacentModel buildCircuit()
        {
            var model = new AdjacentModel(DirectionSet.Cartesian2d);
            var tile1 = new Tile(0);
            var tile2 = new Tile(1);
            model.SetFrequency(tile1, 1);
            model.SetFrequency(tile2, 1);
            model.AddAdjacency(tile1, tile2, 1, 0, 0);
            model.AddAdjacency(tile1, tile2, 0, 1, 0);
            model.AddAdjacency(tile2, tile1, 1, 0, 0);
            model.AddAdjacency(tile2, tile1, 0, 1, 0);
            return model;
        }

        private AdjacentModel generateMountain()
        {
            _adjacencyGen.match_Tiles();
            var model = new AdjacentModel(DirectionSet.Cartesian2d);
            List<Tile> tileList = new List<Tile>();
            foreach (var tile in tileData)
            {
                //create new tile
                //establish  the new frequency
                //add to the list of tiles
                var newTile = new Tile(tile.tileId);
                model.SetFrequency(newTile, 1);
                tileList.Add(newTile);
            }

            for (int i = 0; i < tileData.Count; i++)
            {
                for (int dir = 0; dir < tileData[i].adjacencyPairs.Length; dir++)
                {
                    for (int j = 0; j < tileData[i].adjacencyPairs[dir].Count; j++)
                        model.AddAdjacency(tileList[i], tileList[tileData[i].adjacencyPairs[dir][j]], direction[dir]);
                }
            }

            return model;
        }
    }
}