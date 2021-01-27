import React from "react";
import { hot } from "react-hot-loader/root";
import { Container, Header } from "semantic-ui-react";
import PageSearch from "./page-search"

function App() {
    return (
        <div>
            <div className="topbar">
                <h1 className="text-center">Music Box</h1>
            </div>
            
            <Container>
                <PageSearch/>
            </Container>
        </div>
    );
}

export default hot(App);
