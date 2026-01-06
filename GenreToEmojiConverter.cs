using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Maui.Controls;
using System.Text.Json;

namespace Project2
{
    public class GenreToEmojiConverter : IValueConverter
    {
        static readonly Dictionary<string, string> Map = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Drama", "🎭" },
            { "Crime", "🕵️" },
            { "Action", "🔫" },
            { "Sci-Fi", "👽" },
            { "Science Fiction", "👽" },
            { "Romance", "❤️" },
            { "Comedy", "😂" },
            { "Horror", "👻" },
            { "Animation", "🎨" },
            { "Fantasy", "🧚‍♀️" },
            { "Biography", "👤" },
            { "History", "🏛️" },
            { "Mystery", "🕵️" },
            { "Thriller", "🔪" },
            { "Music", "🎵" },
            { "War", "⚔️" },
            { "Western", "🤠" },
            { "Family", "👪" },
            { "Adventure", "🧭" }
        };

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            IEnumerable<string> genres = Enumerable.Empty<string>();

            // Handle common runtime types that may represent genres
            if (value is IEnumerable<string> stringEnumerable)
            {
                genres = stringEnumerable;
            }
            else if (value is IEnumerable<object> objEnumerable)
            {
                genres = objEnumerable.Select(o => o?.ToString() ?? string.Empty);
            }
            else if (value is JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == JsonValueKind.Array)
                {
                    genres = jsonElement.EnumerateArray()
                        .Select(e => e.ValueKind == JsonValueKind.String ? e.GetString() ?? string.Empty : e.ToString());
                }
                else if (jsonElement.ValueKind == JsonValueKind.String)
                {
                    genres = new[] { jsonElement.GetString() ?? string.Empty };
                }
                else
                {
                    genres = new[] { jsonElement.ToString() };
                }
            }
            else if (value is string s)
            {
                var parts = s.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                genres = parts.Select(p => p.Trim());
            }
            else
            {
                // Fallback to single item string representation
                genres = new[] { value.ToString() ?? string.Empty };
            }

            var emojis = genres
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Select(g => Map.TryGetValue(g.Trim(), out var emoji) ? emoji : "🎬")
                .Distinct();

            return string.Join(" ", emojis);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // One-way converter only.
            return Binding.DoNothing;
        }
    }
}
