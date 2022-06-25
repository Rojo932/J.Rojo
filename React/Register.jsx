import React, {useState} from 'react'
import userService from "../../services/userService"
import toastr from "toastr";

function Register() {

    const [user, setUser] = useState({
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        passwordConfirm: "",
        avatarUrl: "",
        tenantId: "rojo932",
    });

    var onRegisterSuccess = (response) => {
        console.log("onRegisterSuccess >>", response);
        toastr.success("Register Successfull");
      };

      var onRegisterFalure = (response) => {
        console.error(response);
        toastr.error("Register Failed");
      };

    const registerHandler = (event) => {
        event.preventDefault();
        console.log("registerHandler >>", user)
        userService.register(user).then(onRegisterSuccess).catch(onRegisterFalure)
    }

    const onFormFieldChange = event => {
        console.log("onChange", event.currentTarget);
  
        const target = event.currentTarget;
  
        const newUserValue = target.value;
  
        const nameOfField = target.name;
        console.log({nameOfField, newUserValue})
  
        setUser(prevState => {
          console.log("updater onChange");
  
          const newUserObject = {
            ...prevState,
          };
  
          newUserObject[nameOfField] = newUserValue;
  
          return newUserObject;
        });
        console.log("end onChange");
    };

    return (
        <section className="bg-image vh-100" style={{ backgroundImage: `url("https://mdbcdn.b-cdn.net/img/Photos/new-templates/search-box/img4.webp")`}}>
            <div className="mask d-flex align-items-center h-100 gradient-custom-3">
            <div className="container h-100">
                <div className="row d-flex justify-content-center align-items-center h-100">
                <div className="col-12 col-md-9 col-lg-7 col-xl-6">
                    <div className="card">
                    <div className="card-body p-5 cardAp">
                        <h2 className="text-uppercase text-center mb-5">
                        Create an account
                        </h2>

                        <form>
                        <div className="row">
                            <div className="col form-outline mb-4">
                            <input
                                name="firstName"
                                type="text"
                                id="fNameInput"
                                className="form-control form-control-lg"
                                value={user.firstName}
                                onChange={onFormFieldChange}
    
                            />
                            <label className="form-label" htmlFor="firstName">
                                First Name
                            </label>

                            </div>
                            <div className="col form-outline mb-4">
                            <input
                                name="lastName"
                                type="text"
                                id="lNameInput"
                                className="form-control form-control-lg"
                                value={user.lastName}
                                onChange={onFormFieldChange}
    
                            />
                            <label className="form-label" htmlFor="lastName">
                                Last Name
                            </label>
                            </div>
                        </div>

                        <div className="form-outline mb-4">
                            <input
                            name="email"
                            type="email"
                            id="formEmail"
                            className="form-control form-control-lg"
                            value={user.email}
                            onChange={onFormFieldChange}
                            />
                            <label className="form-label" htmlFor="email">
                                Your Email
                            </label>
                        </div>

                        <div className="form-outline mb-4">
                            <input
                            name="password"
                            type="password"
                            id="formPassword"
                            className="form-control form-control-lg"
                            value={user.password}
                            onChange={onFormFieldChange}

                            />
                            <label className="form-label" htmlFor="password">
                                Password - must have( [A-Z], [a-z], [0-9], [#?!$%^*-&], 8-characters )
                            </label>
                        </div>

                        <div className="form-outline mb-4">
                            <input
                            name="passwordConfirm"
                            type="password"
                            id="formConfirmPassword"
                            className="form-control form-control-lg"
                            value={user.passwordConfirm}
                            onChange={onFormFieldChange}
                            />
                            <label className="form-label" htmlFor="passwordConfirm">
                                Repeat your password
                            </label>
                        </div>
                        <div className="form-outline mb-4">
                            <input
                            name="avatarUrl"
                            type="text"
                            className="form-control form-control-lg"
                            id="url1"
                            value={user.avatarUrl}
                            onChange={onFormFieldChange}
                            />
                            <label className="form-label" htmlFor="avatarUrl">
                                Profile URL
                            </label>
                        </div>
                        <div className="d-flex justify-content-center">
                            <button
                            onClick={registerHandler}
                            name="registerBtn"
                            id="registerBtn"
                            type="button"
                            className="btn btn-success btn-block btn-lg gradient-custom-4 text-body">
                            Register
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
    )
}

export default Register
