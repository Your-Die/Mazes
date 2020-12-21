using UnityEngine;

namespace Chinchillada.Generation.Mazes
{
    public static class GridGraphTexture
    {
        public static Texture2D GenerateTexture(this GridGraph grid, Vector2Int cellSize, Vector2Int wallSize,
            Color wallColor)
        {
            var totalSize = cellSize + wallSize;

            var totalCellWidth = grid.Width * cellSize.x;
            var totalCellHeight = grid.Height * cellSize.y;

            var totalWallWidth = (grid.Width + 1) * wallSize.x;
            var totalWallHeight = (grid.Height + 1) * wallSize.y;

            var totalWidth = totalCellWidth + totalWallWidth;
            var totalHeight = totalCellHeight + totalWallHeight;

            var texture = new Texture2D(totalWidth, totalHeight);

            for (var x = 0; x < grid.Width; x++)
            for (var y = 0; y < grid.Height; y++)
            {
                var node = grid[x, y];
                var pixelCoordinate = new Vector2Int(x, y) * totalSize;

                if (node.NorthNeighbor == null)
                    DrawNorth(pixelCoordinate, ref totalSize, ref wallSize, texture, ref wallColor);

                if (node.WestNeighbor == null)
                    DrawWest(pixelCoordinate);
            }

            DrawEastBorder();
            DrawSouthBorder();

            return texture;

            void DrawWest(Vector2Int pixelCoordinate)
            {
                for (var offsetX = 0; offsetX < wallSize.x; offsetX++)
                {
                    var x = pixelCoordinate.x + offsetX;

                    for (var offsetY = 0; offsetY < totalSize.y + 1; offsetY++)
                    {
                        var y = pixelCoordinate.y + offsetY;
                        texture.SetPixel(x, y, wallColor);
                    }
                }
            }

            void DrawSouth(Vector2Int pixelCoordinate)
            {
                pixelCoordinate.y += totalSize.y;
                DrawNorth(pixelCoordinate, ref totalSize, ref wallSize, texture, ref wallColor);
            }

            void DrawEast(Vector2Int pixelCoordinate)
            {
                pixelCoordinate.x += totalSize.x;
                DrawWest(pixelCoordinate);
            }

            void DrawEastBorder()
            {
                var x = grid.Width - 1;
                for (var y = 0; y < grid.Height; y++)
                {
                    var node = grid[x, y];
                    if (node.EastNeighbor != null)
                        continue;

                    var pixelCoordinate = new Vector2Int(x, y) * totalSize;
                    DrawEast(pixelCoordinate);
                }
            }

            void DrawSouthBorder()
            {
                var y = grid.Height - 1;
                for (int x = 0; x < grid.Width; x++)
                {
                    var node = grid[x, y];
                    if (node.SouthNeighbor != null)
                        continue;

                    var pixelCoordinate = new Vector2Int(x, y) * totalSize;
                    DrawSouth(pixelCoordinate);
                }
            }
        }

        private static void DrawNorth(Vector2Int pixelCoordinate, ref Vector2Int totalSize, ref Vector2Int wallSize, Texture2D texture, ref Color wallColor)
        {
            for (var offsetX = 0; offsetX < totalSize.x + 1; offsetX++)
            {
                var x = pixelCoordinate.x + offsetX;

                for (var offsetY = 0; offsetY < wallSize.y; offsetY++)
                {
                    var y = pixelCoordinate.y + offsetY;
                    texture.SetPixel(x, y, wallColor);
                }
            }
        }
    }
}