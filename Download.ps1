param (
    [Parameter(Mandatory = $true)][int]$day
)
 
$dayFormatted = $day.ToString("00")
aoc download -d $day -i .\AdventOfCode\Day$dayFormatted\input.txt -p  .\AdventOfCode\Day$dayFormatted\puzzle.md