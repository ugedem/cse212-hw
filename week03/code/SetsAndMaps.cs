using System.Text.Json;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var result = new List<string>();

        foreach (var w in words)
        {
            var rev = new string(new[] { w[1], w[0] });
            // skip palindromes
            if (w[0] == w[1]) continue;

            if (seen.Contains(rev))
            {
                // w is the reversed version, so format "w & rev"
                result.Add($"{w} & {rev}");
            }
            else
            {
                seen.Add(w);
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');
            if (fields.Length < 4) continue;
            var deg = fields[3].Trim();
            if (degrees.ContainsKey(deg))
                degrees[deg]++;
            else
                degrees[deg] = 1;
        }

        return degrees;
    }


    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // normalize: remove spaces, to lowercase
        var a = word1.Replace(" ", "").ToLowerInvariant();
        var b = word2.Replace(" ", "").ToLowerInvariant();

        if (a.Length != b.Length) return false;

        var counts = new Dictionary<char, int>();
        foreach (var c in a)
        {
            counts[c] = counts.TryGetValue(c, out var cnt) ? cnt + 1 : 1;
        }

        foreach (var c in b)
        {
            if (!counts.TryGetValue(c, out var cnt) || cnt == 0)
                return false;
            counts[c] = cnt - 1;
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
 public static string[] EarthquakeDailySummary()
{
    const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

    using var client = new HttpClient();
    using var stream = client.GetStreamAsync(uri).Result;

    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(stream, options);

    var summary = new List<string>();

    if (featureCollection?.Features != null)
    {
        foreach (var feature in featureCollection.Features)
        {
            var place = feature?.Properties?.Place;
            var magnitude = feature?.Properties?.Magnitude;

            if (!string.IsNullOrWhiteSpace(place) && magnitude.HasValue)
            {
                summary.Add($"{place} - Mag {magnitude.Value:F2}");
            }
        }
    }

    return summary.ToArray();
}
}