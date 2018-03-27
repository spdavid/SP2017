var Gender;
(function (Gender) {
    Gender[Gender["Male"] = 0] = "Male";
    Gender[Gender["Female"] = 1] = "Female";
})(Gender || (Gender = {}));
var Person = (function () {
    function Person(name, weight, age, gender) {
        this.Name = name;
        this.Weight = weight;
        this.Age = age;
        this.Gender = gender;
    }
    Person.prototype.SayHello = function () {
        //jQuery('#result').html("<div>" + "Hello " + this.Name + " " + somevar + "</div>");
        return "Hello " + this.Name + " " + somevar;
    };
    return Person;
}());
var Student = (function () {
    function Student() {
    }
    Student.prototype.getGrade = function () {
        return this.grade;
    };
    return Student;
}());
//# sourceMappingURL=main.js.map