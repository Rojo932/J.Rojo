import React, {useState} from 'react'
import entityService from "./services/entitiesService";
import toastr from "toastr";


function Product() {

    const [product, setState] = useState({
        name:'',
        manufacturer : '',
        description :'',
        cost: '',
    });

    var onProductSuccess = (response) => {
        console.log("onProductSuccess >>", response);
        toastr.success(`Product Add Successfull ID:${response.id}`);
      };

      var onProductFalure = (response) => {
        console.error(response);
        toastr.error("Product Add Failed");
      };


    const productHandler = (event) => {
        event.preventDefault();
        console.log("productHandler >>", product)
        entityService.add(product, "products").then(onProductSuccess).catch(onProductFalure)
    }


    const onFormFieldChange = event => {
        console.log("onChange", event.currentTarget);
  
        const target = event.currentTarget;
  
        const newProductValue = target.value;
  
        const nameOfField = target.name;
        console.log({nameOfField, newProductValue})
  
        setState(prevState => {
          console.log("updater onChange");
  
          const newProductObject = {
            ...prevState,
          };
  
          newProductObject[nameOfField] = newProductValue;
  
          return newProductObject;
        });
        console.log("end onChange");
    };


  return (
    <React.Fragment>
      <section
        className="bg-image vh-100"
        style={{
          backgroundImage: `url("https://mdbcdn.b-cdn.net/img/Photos/new-templates/search-box/img4.webp")`,
        }}
      >
        <div className="mask d-flex align-items-center h-100 gradient-custom-3">
          <div className="container h-100">
            <div className="row d-flex justify-content-center align-items-center h-100">
              <div className="col-12 col-md-9 col-lg-7 col-xl-6">
                <div className="card">
                  <div className="card-body p-5 cardAp">
                    <h2 className="text-uppercase text-center mb-5">
                        Create Product
                    </h2>
                    <form>
                      <div className="form-outline mb-4">
                        <input
                          name="name"
                          type="text"
                          id="nameInput1"
                          className="form-control form-control-lg"
                          value={product.name}
                          onChange={onFormFieldChange}
                        />
                        <label className="form-label" htmlFor="name">
                          Name
                        </label>
                      </div>
                      <div className="form-outline mb-4">
                        <input
                          name="manufacturer"
                          type="text"
                          id="manufacturerInput1"
                          className="form-control form-control-lg"
                          value={product.manufacturer}
                          onChange={onFormFieldChange}
                        />
                        <label className="form-label" htmlFor="manufacturer">
                        Manufacturer
                        </label>
                      </div>
                      <div className="form-outline mb-4">
                        <input
                          name="description"
                          type="text"
                          id="descriptionInput1"
                          className="form-control form-control-lg"
                          value={product.description}
                          onChange={onFormFieldChange}
                        />
                        <label className="form-label" htmlFor="description">
                        Description
                        </label>
                      </div>
                      <div className="form-outline mb-4">
                        <input
                          min="0.00" 
                          max="10000.00" 
                          step="0.01"
                          type="number"
                          name="cost"
                          id="costInput1"
                          className="form-control form-control-lg"
                          value={product.cost}
                          onChange={onFormFieldChange}
                        />
                        <label className="form-label" htmlFor="cost">
                        Cost ( USD )
                        </label>
                      </div>

                      <div className="pt-1 mb-4">
                        <button
                          onClick={productHandler}
                          name="submitProduct"
                          className="btn btn-info btn-lg btn-block"
                          type="button"
                          id="submitProduct"
                        >
                          Submit
                        </button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </React.Fragment>
  )
}

export default Product
