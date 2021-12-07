open System.IO

type FishAge = FishAge of int32 with
    static member Zero = FishAge 0
    static member (-) (FishAge a, FishAge b) = FishAge (a - b)

type PopulationSize = PopulationSize of int64 with
    static member Zero = PopulationSize 0L
    static member (+) (PopulationSize a, PopulationSize b) = PopulationSize (a + b)

type PopulationSegment = { Age: FishAge; Size: PopulationSize }

let tokens = File.ReadAllText("input.txt").Split(',')

let population =
    tokens
    |> Seq.map int32
    |> Seq.groupBy id
    |> Seq.map (fun group ->
        let age = fst group
        let size = snd group |> Seq.length |> int64
        { Age = FishAge age; Size = PopulationSize size }
    )

let evolve population =
    let fishAboutToSpawn = population |> Seq.tryFind (fun p -> p.Age = FishAge.Zero)

    let newFish = {
        Age = FishAge 8
        Size = fishAboutToSpawn |> Option.map (fun p -> p.Size) |> Option.defaultValue PopulationSize.Zero
    }

    population
    |> Seq.map (fun p -> {
        Age = if p.Age > FishAge.Zero then p.Age - FishAge 1 else FishAge 6
        Size = p.Size;
    })
    |> Seq.insertAt 0 newFish
    |> Seq.groupBy (fun popn -> popn.Age)
    |> Seq.map (fun group ->
        let age = fst group
        let size = snd group |> Seq.map (fun p -> p.Size) |> Seq.sum
        { Age = age; Size = size })

let rec solve days population =
    if days = 0 then
        population |> Seq.map (fun pair -> pair.Size) |> Seq.sum
    else
        solve (days-1) (evolve population)

printfn $"Part 1: {solve 80 population}"
printfn $"Part 2: {solve 256 population}"
