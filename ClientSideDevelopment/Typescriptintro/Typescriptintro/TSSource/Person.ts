declare var somevar: string;




     enum Gender {
        Male,
        Female
    } 

     class Person {
        Name: string;
        Weight: number;
        Age: number;
        Gender: Gender;
          
   

        constructor(name: string, weight: number, age: number, gender: Gender) {

            this.Name = name; 
            this.Weight = weight;
            this.Age = age;
            this.Gender = gender; 
        }

        public SayHello(): string {

            
            //jQuery('#result').html("<div>" + "Hello " + this.Name + " " + somevar + "</div>");
            return "Hello " + this.Name + " " + somevar;
        }


    }



