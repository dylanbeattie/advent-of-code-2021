open System.IO
type FishAge = FishAge of int32
type PopulationSize = PopulationSize of int64
type PopulationSegment = { Age: FishAge; Size: PopulationSize }

let tokens = File.ReadAllText("input.txt").Split(',')
let population = 
    tokens
    |> Seq.map int32    
    |> Seq.groupBy (fun age -> age)
    |> Seq.map (fun group -> 
        let age = fst group
        let size = snd group |> Seq.length
        { Age = FishAge age; Size = PopulationSize size })

let evolve population = 
    let fishAboutToSpawn = population |> Seq.tryFind (fun p -> p.Age = 0)
    let newFish = { Age = 8; Size = if fishAboutToSpawn.IsNone then 0 else fishAboutToSpawn.Value.Size }    
    population 
    |> Seq.map (fun p -> { Age = (if p.Age > 0 then p.Age - 1 else 6); Size = p.Size; })
    |> Seq.insertAt 0 newFish
    |> Seq.groupBy (fun popn -> popn.Age)
    |> Seq.map (fun group -> 
        let age = fst group
        let size = snd group |> Seq.map (fun p -> p.Size) |> Seq.sum |> PopulationSize 
        { Age = age; Size = size })

let rec solve days population = 
    if days = 0 then 
        population |> Seq.map (fun pair -> pair.Size) |> Seq.sum 
    else
        solve (days-1) (evolve population)
    
printfn "Part1: %i" (solve 80 population)
printfn "Part 2: %i" (solve 256 population)