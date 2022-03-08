import './Modal.css';

const Modal = ({ handleClose, show, showOk, children }) => {
    const showHideClassName = show ? "modal display-block" : "modal display-none";
    return (
        <div className={showHideClassName}>
            <section className="modal-main">
                {children}

                {showOk ?

                    <button type="button" className="btn btn-secondary btn-lg w-100 mt-3" onClick={handleClose}>
                        Ok
                    </button>
                    :

                    <button type="button" className="btn btn-secondary btn-lg w-100 mt-3" onClick={handleClose}>
                        Annuler
                    </button>}
            </section>
        </div>
    );
};
export default Modal;