import React, { Component } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter, CardBody } from 'reactstrap';

export class ItemDetails extends Component {

    constructor(props) {
        super(props);
        this.state = {
            isShow: false
        }

        this.handleClose = this.handleClose.bind(this)
        this.toggle = this.toggle.bind(this)
    }

    handleClose() {
        this.setState({
            isShow: false
        })
    }

    toggle () {
        this.setState(prevState => ({
            isShow: !prevState.isShow
        }))
    }

    render() {
        return (
            <>
                {/* <Modal isOpen={this.state.isShow} onHide={this.handleClose}>
                    <Modal.Header toggle={this.toggle} className="">
                        <Modal.Title>aaa</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Modal.Card>
                            <CardBody>
                                
                            </CardBody>
                        </Modal.Card>
                    </Modal.Body>
                </Modal> */}
            </>
        )
    }
}
