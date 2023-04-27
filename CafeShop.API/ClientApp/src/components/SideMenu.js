import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faClose } from '@fortawesome/free-solid-svg-icons'
import { Modal, ModalHeader, ModalBody, ModalFooter, CardBody } from 'reactstrap';

export class SideMenu extends Component {

    constructor(props) {
        super(props);
        console.log(this.props.isShowSideMenu)
        this.state = {
            showMenu: this.props.isShowSideMenu,
            categories: this.props.categories,
            selectedCategoryId: this.props.selectedCategoryId
        };
    }

    componentDidMount() {
        //this.populateWeatherData();
    }


    render() {
        return (
            <section id="side-menu" className={"modal fade-left modal-menu " + (this.props.isShowSideMenu ? "in show" : '')} tabIndex="-1" role="dialog">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h6 className="modal-title" style={{ fontWeight: 700 }}>Danh Mục</h6>
                            <button type="button" onClick={this.props.updateIsShowSideMenu} className="close" data-dismiss="modal"><FontAwesomeIcon icon={faClose} color='red' /></button>
                        </div>
                        <div className="list-group side-group">
                            {
                                this.props.categories.map(cate => (
                                    <a key={cate.id} className='list-group-item side-group-item'>{cate.name}</a>
                                ))
                            }
                        </div>
                    </div>
                </div>

            </section>
        );
    }
}
