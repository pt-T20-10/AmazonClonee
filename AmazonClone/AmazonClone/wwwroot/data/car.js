
class Car{
  #brand;
  #model;
  speed = 0;
  isTrunkOpen = false;
  constructor(detials){
    this.#brand = detials.brand;
    this.#model = detials.model;
  }
  displayInfo(){
     console.log( `${this.#brand} ${this.#model}, Speed: ${this.speed}km/h
                  ${this.isTrunkOpen?'The trunk is open':'The trunk is close'}`);
  }
  go(){
    if(this.speed >200) alert('The car has reached its maximum speed.');
    else  if(this.isTrunkOpen) console.log('The trunk is openning, can not go');
    else
    this.speed +=5;
  }
  break(){
    if(this.speed <= 0) alert('The car is currently parked');
    else this.speed -= 5;
  }
  openTrunk(){
    if(this.speed === 0) console.log('This car is running, can not open the trunk');
    else
    this.isTrunkOpen = true;
  }
  closeTrunk(){

    this.isTrunkOpen = false;
  }
}

class RaceCar extends Car{
  acceleration;
  constructor(detials){
    super(detials);
    this.acceleration = detials.acceleration;
  }
  go(){
    this.speed += this.acceleration;
    if( this.speed > 300) {this.speed = 300};
  }
  openTrunk(){
    console.log('Racer cars do not have a trunk');
    
  }
  closeTrunkTrunk(){
    console.log('Racer cars do not have a trunk');
    
  }
  displayInfo(){
    console.log(`${this.brand} ${this.model}, Speed: ${this.speed}km/h, Acceleration: ${this.acceleration} `);
  }

}
const car1 = new Car({
  brand: 'Toyota',
  model: 'Corolla'

})
const car2 = new Car({
  brand: 'Tesla',
  model: 'Model 3'
})

const raceCar = new RaceCar({
  brand: 'McLaren',
  model: 'F1',
  acceleration: 20
})
/*raceCar.go();
raceCar.break();
raceCar.displayInfo();*/
car1.go();

car2.go();
car1.displayInfo();
car2.displayInfo();



