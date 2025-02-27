using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    internal class BoxFactory
    {
        public IBox? Create(string input)
        {
            var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            return ProcessLines(lines);
        }

        private IBox? ProcessLines(List<string> lines)
        {
            if (lines.Count == 0) return null;

            string first = lines[0];
            lines.RemoveAt(0);

            var parts = first.Split(" ", 2);

            switch (parts[0])
            {
                case "mono":
                    return new MonoBox(parts.Length > 1 ? parts[1] : "");

                case "cv":
                    IBox? top = ProcessLines(lines);
                    IBox? bottom = ProcessLines(lines);
                    return (top != null && bottom != null) ? new ComboVertical(new Box(top), new Box(bottom)) : null;

                case "ch":
                    IBox? left = ProcessLines(lines);
                    IBox? right = ProcessLines(lines);
                    return (left != null && right != null) ? new ComboHorizontal(new Box(left), new Box(right)) : null;

                default:
                    throw new InvalidOperationException($"Commande inconnue: {first}");
            }
        }
    }
}