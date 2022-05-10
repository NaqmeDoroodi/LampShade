const cookieName = "cart-items";

function addToCart(id, name, slug, unitePrice, img) {

    let products = $.cookie(cookieName);
    if (products === undefined) products = [];
    else products = JSON.parse(products);

    const count = $("#productCount").val();

    const currentProductInCookie = products.find(x => x.id === id);
    if (currentProductInCookie === undefined) {
        const product = { id, name, slug, unitePrice, img, count };
        products.push(product);
    } else currentProductInCookie.count = parseInt(currentProductInCookie.count) + parseInt(count);

    $.cookie(cookieName, JSON.stringify(products), { expires: 10, path: "/" });
    updateCart();
}


function updateCart() {

    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart-items-count").text(products.length);

    const wrapper = $("#cart-items-wrapper");
    wrapper.html(" ");

    products.forEach(p =>
    {
        {
            const product = `      <div class="single-cart-item">
                                <a class="remove-icon" onclick="removeFromCart(${p.id})">
                                    <i class="ion-android-close"></i>
                                </a>
                                <div class="image">
                                    <a asp-page="/Product"  asp-route-id="${p.slug}">
                                        <img src="/ProductPictures/${p.img}" class="img-fluid" alt="${p.name
                }" title="${p.name}">
                                    </a>
                                </div>
                                <div class="content">
                                    <p class="product-title">
                                        <a asp-page="/Product" asp-route-id="${p.slug}">${p.name}</a>
                                    </p>
                                    <p class="count">تعداد: ${p.count}</p>
                                    <p class="count">قیمت واحد: ${p.unitePrice}</p>
                                </div>
                            </div>`;
            wrapper.append(product);
        }
    });
}

function removeFromCart(id) {
    debugger;
    let products = $.cookie(cookieName);
    products = JSON.parse(products);

    const itemToRemove = products.findIndex(x => x.id == id);

    products.splice(itemToRemove, 1);

    $.cookie(cookieName, JSON.stringify(products), { expires: 10, path: "/" });
    updateCart();
}


function changeCount(id, totalId, count) {
    var products = $.cookie(cookieName);
    products = JSON.parse(products);

    const productIndex = products.findIndex(x => x.id == id);
    const product = products[productIndex];

    product.count = count;
    const newPrice = parseInt(product.unitePrice) * parseInt(count);
    $(`#${totalId}`).text(`${newPrice} تومان`);

    $.cookie(cookieName, JSON.stringify(products), { expires: 10, path: "/" });
    updateCart();


    const settings = {
        "url": "https://localhost:5001/api/inventory",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "productId": id, "count": count })
    };
    $.ajax(settings).done(function (data) {
        var d = data;
        if (data.isInStock == false) {
            const warningsDiv = $('#productStockWarning');
            if ($(`#${id}`).length == 0) {
                warningsDiv.append(`
                    <div class="alert alert-warning" id="${id}">
                        <i class="fa fa-warning"></i> کالای
                        <strong>${data.productName}</strong>
                        در انبار کمتر از تعداد درخواستی موجود است.
                    </div>
                `);
            }
        } else {
            if ($(`#${id}`).length > 0) {
                $(`#${id}`).remove();
            }
        }
    });
}