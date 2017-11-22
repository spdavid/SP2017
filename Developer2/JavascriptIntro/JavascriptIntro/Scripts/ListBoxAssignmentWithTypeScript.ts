

function moveItem(direction) {
    
    //  console.log(direction);
    var sourceSelect : HTMLSelectElement = null;
    var destinationSelect: HTMLSelectElement = null;

    //console.log(leftSelect);
    //console.log(rightSelect);

    if (direction === "right") {
        sourceSelect = document.getElementById("leftSelect") as HTMLSelectElement;
        destinationSelect = document.getElementById("rightSelect") as HTMLSelectElement;
    }
    else {
        sourceSelect = document.getElementById("rightSelect") as HTMLSelectElement;
        destinationSelect = document.getElementById("leftSelect") as HTMLSelectElement;
    }

    console.log(sourceSelect.selectedIndex);
    console.log(sourceSelect.options);

    var selectedItem = sourceSelect.options[sourceSelect.selectedIndex];
    console.log(selectedItem);
    destinationSelect.options.add(selectedItem);

   
}



function moveItemMulti(direction) {
    //  console.log(direction);
    var sourceSelect = null;
    var destinationSelect = null;

    //console.log(leftSelect);
    //console.log(rightSelect);

    if (direction === "right") {
        sourceSelect = document.getElementById("leftSelectMulti");
        destinationSelect = document.getElementById("rightSelectMulti");
    }
    else {
        sourceSelect = document.getElementById("rightSelectMulti");
        destinationSelect = document.getElementById("leftSelectMulti");
    }

    for (var i = sourceSelect.options.length - 1; i >= 0; i--) {
        var option = sourceSelect.options[i];

        console.log(option);
        console.log(option.selected);
        if (option.selected) {
            destinationSelect.options.add(option);
        }

    }
    
}