 class Person
{
    Name: string;
    Age: number;
    Height: number;
    foo: string;
     
    constructor(name : string, age : number, height : number)
    { 
        this.Name = name;
        this.Age = age;
        this.Height = height;
    }

    getInfo() : string
    {
        return this.Name + " " + this.Age.toString() + " " + this.Height.toString();

    }
     
}


