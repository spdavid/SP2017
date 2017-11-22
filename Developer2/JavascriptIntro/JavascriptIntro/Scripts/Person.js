var Person = (function () {
    function Person(name, age, height) {
        this.Name = name;
        this.Age = age;
        this.Height = height;
    }
    Person.prototype.getInfo = function () {
        return this.Name + " " + this.Age.toString() + " " + this.Height.toString();
    };
    return Person;
}());
//# sourceMappingURL=Person.js.map