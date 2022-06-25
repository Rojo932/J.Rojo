import React, {useState, useEffect} from 'react'
import { useLocation } from 'react-router-dom';
import toastr from "toastr";
import friendsService from "../../services/friendsService"
import {useNavigate} from "react-router-dom"

function FormAddUpdate() {

  const navigate = useNavigate();
  const location = useLocation()
  const friendData = location.state;
  const formTitle = friendData? "Update": "Add" 

  useEffect(() =>{
    if(friendData){

      setState((prevState)=> {
        friendData.payload.primaryImage = friendData.payload.primaryImage.imageUrl
        return {...prevState, ...friendData.payload}  //old Data first  >> new Data Second
      })
    }
    
  },[friendData])
  
    const [friend, setState] = useState({
      title: "",
      bio: "",
      summary: "",
      headline: "",
      slug: "",
      statusId: "Active",
      primaryImage: "",
    });

    const onAddSuccess = (response) => {
        console.log("onRegisterSuccess >>", response);
        toastr.success("Add Successfull");
        navigate(`/friends`)
      };

    const onAddFalure = (response) => {
        console.error(response);
        toastr.error("Add Failed");
      };

    const onUpdateSuccess = (response) => {
      console.log("onUpdateSuccess >>", response);
      toastr.success("Update Successfull");
      navigate(`/friends`)
    }

    const onUpdateFalure = (response) => {
      console.error(response);
      toastr.error("Update Failed");
    }

    const addUpdateHandler = (event) => {
        event.preventDefault();
        if(location.state){
          friendsService.update(friend, friend.id).then(onUpdateSuccess).catch(onUpdateFalure)
        }
        else{
          console.log("registerHandler >>", friend)
          friendsService.add(friend).then(onAddSuccess).catch(onAddFalure)
        }
    }

    const onFormFieldChange = event => {
  
        const target = event.currentTarget;
  
        const newFriendValue = target.value;
  
        const nameOfField = target.name;

        setState(prevState => {
          const newFriendObject = {
            ...prevState,
          };
  
          newFriendObject[nameOfField] = newFriendValue;
  
          return newFriendObject;
        });
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
                    <form>
                      <h3 className="fw-normal mb-3 pb-3">{formTitle} Friend</h3>
                      <div className="form-outline mb-4">
                            <input
                            name="title"
                            type="text"
                            id="titleInp"
                            className="form-control form-control-lg"
                            value={friend.title}
                            onChange={onFormFieldChange}
                            />
                            <label className="form-label" htmlFor="title">
                            Title
                            </label>
                        </div>
                        <div className="form-outline mb-4">
                            <input
                            name="bio"
                            type="text"
                            id="formBio"
                            className="form-control form-control-lg"
                            value={friend.bio}
                            onChange={onFormFieldChange}
                            />
                            <label className="form-label" htmlFor="bio">
                              Bio
                            </label>
                        </div>
                        <div className="form-outline mb-4">
                            <input
                            name="summary"
                            type="text"
                            id="formSummary"
                            className="form-control form-control-lg"
                            value={friend.summary}
                            onChange={onFormFieldChange}
                            />
                            <label className="form-label" htmlFor="summary">
                              Summary
                            </label>
                        </div>

                        <div className="form-outline mb-4">
                            <input
                            name="headline"
                            type="text"
                            id="formHeadline"
                            className="form-control form-control-lg"
                            value={friend.headline}
                            onChange={onFormFieldChange}

                            />
                            <label className="form-label" htmlFor="headline">
                              Headline
                            </label>
                        </div>

                        <div className="form-outline mb-4">
                            <input
                            name="slug"
                            type="text"
                            id="formSlug"
                            className="form-control form-control-lg"
                            value={friend.slug}
                            onChange={onFormFieldChange}
                            />
                            <label className="form-label" htmlFor="slug">
                             Slug
                            </label>
                        </div>
                        <div className="form-outline mb-4">
                            <input
                            name="primaryImage"
                            type="text"
                            className="form-control form-control-lg"
                            id="primaryImage"
                            value={friend.primaryImage}
                            onChange={onFormFieldChange}
                            />
                            <label className="form-label" htmlFor="primaryImage">
                                Profile URL
                            </label>
                        </div>
                        <div className="d-flex justify-content-center">
                            <button
                            onClick={addUpdateHandler}
                            name="addUpdate"
                            id="addUpdate"
                            type="button"
                            className="btn btn-success btn-block btn-lg gradient-custom-4 text-body">
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

export default FormAddUpdate
