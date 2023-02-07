import React from "react";
import { useNavigate } from "react-router";
import { input, label } from 'react-bootstrap';

export default function LoginPage() {
    const navigate = useNavigate();
    return (
        <>
            <div class="container p-5">
                <div class="card">
                    <div class="card-body">
                    <form>
                    <div class="mx-5 mt-5">
                        <label for="exampleInputEmail1" class="form-label">Email</label>
                        <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" />
                        <div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>
                    </div>
                    <div class="mx-5">
                        <label for="exampleInputPassword1" class="form-label">Password</label>
                        <input type="password" class="form-control" id="exampleInputPassword1" />
                    </div>
                    
                    <button type="submit" class="btn btn-primary m-5">Submit</button>
                </form>
                    </div>
                </div>
                
            </div>
        </>
    )
}