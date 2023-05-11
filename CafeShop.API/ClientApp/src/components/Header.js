import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCoffee, faBars } from '@fortawesome/free-solid-svg-icons'

export class Header extends Component {

    constructor(props) {
        super(props);

        this.showSideMenu = this.showSideMenu.bind(this);
    }

    componentDidMount() {
        //this.populateWeatherData();
    }

    showSideMenu() {
        this.props.updateIsShowSideMenu(true)
    }


    render() {
        return (
            <header className="container-fluid" id="header">
                <div className="container">
                    <a id="bar" onClick={this.showSideMenu} data-toggle="modal" data-target="#side-menu" title="Menu" href="#"><FontAwesomeIcon icon={faBars} /></a>
                    <h4 className="page-header">My cafe</h4>
                    <a title="Home" className="pull-right" href="/"><FontAwesomeIcon icon={faCoffee} /></a>
                </div>
            </header>
        );
    }
}
