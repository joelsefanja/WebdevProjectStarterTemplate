function addToOrder(product, amount) {
    addToOrderAsync(product, amount);
    var orderItems = document.getElementById('orderItems');
    var existingItem = orderItems.querySelector('tr[data-product="' + product + '"]');

    if (existingItem) {
        // Verhoog het aantal als het product al in de bestelling staat
        var amountElement = existingItem.querySelector('.amount');
        var currentAmount = parseInt(amountElement.innerText);
        amountElement.innerText = currentAmount + amount;
    } else {
        // Voeg een nieuw item toe aan de bestelling
        var newRow = document.createElement('tr');
        newRow.setAttribute('data-product', product);
        newRow.innerHTML = `
        <td>${product}</td>
        <td class="amount">${amount}</td>
        <td class="actions">
          <button class="btn btn-sm btn-warning" onclick="updateAmount('${product}', -1)">-</button>
          <button class="btn btn-sm btn-success" onclick="updateAmount('${product}', 1)">+</button>
          <button class="btn btn-sm btn-danger" onclick="removeFromOrder('${product}')">&#128465;</button>
        </td>`;
        orderItems.appendChild(newRow);
    }
}

async function addToOrderAsync(productId, quantity) {
    $.ajax({
        url: '@Url.Page("/Order", "AddToOrder")',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ productId: productId, quantity: quantity }),
        success: function (response) {
            if (response.success) {
                alert('Product successfully added to order. Total: ' + response.total);
            } else {
                alert('Failed to add product to order.');
            }
        },
        error: function (xhr, status, error) {
            alert('Error adding product to order: ' + error);
        }
    });
}

function updateAmount(product, delta) {
    var orderItems = document.getElementById('orderItems');
    var existingItem = orderItems.querySelector('tr[data-product="' + product + '"]');

    if (existingItem) {
        // Update het aantal met het opgegeven verschil (delta)
        var amountElement = existingItem.querySelector('.amount');
        var currentAmount = parseInt(amountElement.innerText);
        var newAmount = currentAmount + delta;

        if (newAmount === 0) removeFromOrder(product)

        amountElement.innerText = newAmount;

    }
}

/**
 * Remove a product from the order list.
 *
 * @param {string} product - The name of the product to be removed.
 */
function removeFromOrder(product) {
    // Get the order items container
    var orderItems = document.getElementById('orderItems');

    // Find the existing item with the specified product
    var existingItem = orderItems.querySelector('tr[data-product="' + product + '"]');

    // If the existing item is found, remove it from the order
    if (existingItem) {
        existingItem.remove();
    }
}