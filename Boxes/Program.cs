using Boxes;
static void TestFabriques()
{
    var p = new BoxFactory().Create("mono J'aime mon \"prof\"");
    Console.WriteLine(new Box(p));
    p = new BoxFactory().Create("cv\nmono J'aime mon \"prof\"\nmono moi itou");
    Console.WriteLine(new Box(p));
    p = new BoxFactory().Create("ch\nmono J'aime mon \"prof\"\nmono moi itou");
    Console.WriteLine(new Box(p));
    p = new BoxFactory().Create(
        "ch\ncv\nmono J'aime mon \"prof\"\nmono moi itou\nmono eh ben");
    Console.WriteLine(new Box(p));
}
TestFabriques();