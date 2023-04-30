import React, { useContext } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faClose } from '@fortawesome/free-solid-svg-icons'
import { Modal, ModalHeader, ModalBody, ModalFooter, CardBody } from 'reactstrap';
import { CategoriesContext } from './Home'

export function SideMenu(props) {
    const { categoriesList } = useContext(CategoriesContext)

    return (
        <section id="side-menu" className={"modal fade-left modal-menu " + (props.isShowSideMenu ? "in show" : '')} tabIndex="-1" role="dialog">
            <div className="modal-dialog" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h6 className="modal-title" style={{ fontWeight: 700 }}>Danh Mục</h6>
                        <button type="button" onClick={props.updateIsShowSideMenu} className="close" data-dismiss="modal"><FontAwesomeIcon icon={faClose} color='red' /></button>
                    </div>
                    <div className="list-group side-group">
                        {
                            categoriesList.map(cate => (
                                <a key={cate.cateId} className='list-group-item side-group-item'>{cate.name}</a>
                            ))
                        }
                    </div>
                </div>
            </div>

        </section>
    );
}
