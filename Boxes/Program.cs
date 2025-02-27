using Boxes;
static void FactoryTest()
{
    var p = new BoxFactory().Create("mono J'aime mon \"prof\"");
    Console.WriteLine(new Box(p));
    p = new BoxFactory().Create("cv\nmono J'aime mon \"prof\"\nmono moi itou");
    Console.WriteLine(new Box(p));
    p = new BoxFactory().Create("ch\nmono J'aime mon \"prof\"\nmono moi itou");
    Console.WriteLine(new Box(p));
    //p = new BoxFactory().Create(
    //    "ch\ncv\nmono J'aime mon \"prof\"\nmono moi itou\nmono eh ben");
    //Console.WriteLine(new Box(p));
    //p = new BoxFactory().Create(
    //    "ch\ncv\nmc\nmono J'aime mon \"prof\"\nmono moi itou\nmono eh ben");
    //Console.WriteLine(new Box(p));
}
FactoryTest();
//Box b = new();
//Console.WriteLine(b);
////Console.WriteLine(new Box("yo"));
//string texte = @"Man! Hey!!!
//ceci est un test
//multiligne";
//string autTexte = "Ceci\nitou, genre";
//Box b0 = new(texte);
//Box b1 = new(autTexte);
////Console.WriteLine(b0);
////Console.WriteLine(b1);
//ComboVertical cv = new(b0, b1);
////Console.WriteLine(new Box(cv));

//ComboHorizontal ch = new(b0, b1);
//Console.WriteLine(new Box(ch));
//ComboVertical cvplus = new(new Box(cv), new Box(ch));
//Console.WriteLine(new Box(cvplus));
//ComboHorizontal chplus = new(new Box(cv), new Box(ch));
//Console.WriteLine(new Box(chplus));
//ComboVertical cvv = new(new Box(chplus), new Box("coucou"));
//Console.WriteLine(new Box(cvv));
//Console.WriteLine(new Box(
//        new ComboHorizontal(
//            new Box("a\nb\nc\nd\ne"),
//            new Box(
//                new ComboVertical(
//                    new Box("allo"), new Box("yo")
//                )
//            )
//        )
//    )
//);
//Console.WriteLine(
//    new Box(new ComboHorizontal(new Box("Yo"), new Box()))
//);
//Console.WriteLine(
//    new Box(new ComboHorizontal(new Box(), new Box("Ya")))
//);
//Console.WriteLine(
//    new Box(new ComboHorizontal(new Box(), new Box()))
//);
//Console.WriteLine(
//    new Box(new ComboVertical(new Box(), new Box()))
//);
//Console.WriteLine(
//    new Box(new ComboVertical(new Box("Yip"), new Box()))
//);
//Console.WriteLine(
//    new Box(new ComboVertical(new Box(), new Box("Yap")))
//);