import React, { Component } from 'react';
import Modal from './Modal.js';

export class Meals extends Component {

    static displayName = Meals.name;

    constructor(props) {
        super(props);
        this.state = {
            checkCount: 0,
            listMeals: [], loading: true, hideDel: true,
            Meal: { MealId: 0, MealName: "", Price: 0.0, CategoryId: 1, ProviderId: 1 },
            providersList: [], categoriesList: [], chkbx: []
        };
    }

    handleSubmit = () => {

        const postToCreate = {
            MealId: this.state.Meal.MealId === 0 ? 0 : this.state.Meal.MealId,
            MealName: this.state.Meal.MealName,
            Category: { CategoryId: this.state.Meal.CategoryId, CategoryName: "" },
            Provider: { ProviderId: this.state.Meal.ProviderId, ProviderName: "" },
            Price: parseFloat(this.state.Meal.Price)
        };
        if (postToCreate.MealName === "" ||
            postToCreate.Category.CategoryId === 0 ||
            postToCreate.Provider.ProviderId === 0 ||
            postToCreate.Price === 0
        ) {
            this.setState({ showError: true });
        } else {
            const url = 'meal';
            if (postToCreate.MealId === 0) {
                fetch(url, {
                    method: 'POST',
                    headers: {
                        'content-type': 'application/json'
                    },
                    body: JSON.stringify(postToCreate)
                })
                    .then(response => response.json())
                    .then(responseFromServer => {
                        if (responseFromServer === 'false') {
                            this.setState({ showErrorBack: true });
                        } else {
                            this.setState({ showSucces: true });
                            this.populateMealsData();
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                        alert(error);
                    });
            } else {
                fetch(url, {
                    method: 'PUT',
                    headers: {
                        'content-type': 'application/json'
                    },
                    body: JSON.stringify(postToCreate)
                })
                    .then(response => response.json())
                    .then(responseFromServer => {
                        if (responseFromServer === 'false') {
                            this.setState({ showErrorBack: true });
                        } else {
                            this.setState({ showSucces: true });
                            this.populateMealsData();
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                        alert(error);
                    });
            }
        }
    }

    handleConfirmDel = () => {
        this.setState({ showConfirmDel: true });
    }

    handleDel = () => {
        this.setState({ showConfirmDel: false });

        this.state.chkbx.map(id =>
            fetch('meal/' + id, {
                method: 'DELETE'
            })
                .then(response => response.json())
                .then(responseFromServer => {
                    if (responseFromServer === 'false') {
                        this.setState({ showError: true });
                    } else {
                        this.setState({ chkbx: [], hideDel: true, showDel: true });
                        this.populateMealsData();
                    }
                })
                .catch((error) => {
                    console.log(error);
                    alert(error);
                })
        )
    }

    action = (m) => {
        var meal = m["meal"];
        meal.ProviderId = meal["Provider"].ProviderId;
        meal.CategoryId = meal["Category"].CategoryId;
        this.setState({ Meal: meal });
        this.showModal();
    }

    add = () => {
        this.setState({ Meal: { MealId: 0, MealName: "", Price: 0.0, CategoryId: 1, ProviderId: 1 } });
        this.showModal();
    }

    checkDel = (c, name) => {
        var count = this.state.checkCount;

        if (c) {
            count += 1;
            this.setState({ checkCount: count });
            this.setState({ hideDel: false });
            console.log(this.state.checkCount);
            var add = this.state.chkbx.concat(name);
            this.setState({ chkbx: add });
        } else {
            count -= 1;
            this.setState({ checkCount: count });
            var array = [...this.state.chkbx];
            var index = array.indexOf(name);
            if (index !== -1) {
                array.splice(index, 1);
                this.setState({ chkbx: array });
            }
            console.log(this.state.chkbx);
        }
        if (count === 0) {
            this.setState({ hideDel: true });
        }
    }

    showModal = () => {
        this.setState({ show: true });
    };

    hideModal = () => {

        if (this.state.showError === true) {
            this.setState({ showError: false });
        } else {
            this.setState({ show: false, showSucces: false, showDel: false, showError: false, showConfirmDel: false });
        }
    };

    componentDidMount = () => {
        this.populateProviders();
        this.populateCategories();
        this.populateMealsData();
    }

    onMealNameChange = (value) => {
        var meal = this.state.Meal;
        meal.MealName = value;
        this.setState({ meal: meal });
        this.populateMealsData();
    }

    onCategoryChange = (value) => {
        console.log(value);
        var meal = this.state.Meal;
        meal.CategoryId = value;
        this.setState({ meal: meal });
        this.populateMealsData();
    }

    onProviderChange = (value) => {
        var meal = this.state.Meal;
        meal.ProviderId = value;
        this.setState({ meal: meal });
        this.populateMealsData();
    }

    onPriceChange = (value) => {
        var meal = this.state.Meal;
        meal.Price = value;
        this.setState({ meal: meal });
        this.populateMealsData();
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th></th>
                        <th>Nom</th>
                        <th>Fournisseur</th>
                        <th>Catégorie</th>
                        <th>Prix</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.listMeals.map(meal =>
                        <tr key={meal.MealId}>
                            <td><input ref="chkbx" name={meal.MealId} className="chkbx" type="checkbox" onChange={e => this.checkDel(e.target.checked, e.target.name)} /></td>
                            <td>{meal.MealName}</td>
                            <td>{meal.Provider.ProviderName}</td>
                            <td>{meal.Category.CategoryName}</td>
                            <td>{meal.Price}</td>
                            <td><button onClick={() => this.action({ meal })}>/</button></td>
                        </tr>
                    )}
                </tbody>
            </table>

        return (
            <div>
                <h1 id="tabelLabel" >Gestion des plats</h1>
                <button type="button" onClick={this.add} className="btn btn-dark w-15 mt-5">Ajouter +</button>
                {this.state.hideDel ? "" :
                    <button className="btn btn-secondary w-15 mt-5" onClick={this.handleConfirmDel}>Supprimer -</button>}
                {contents}
                <Modal show={this.state.show} handleClose={this.hideModal}>
                    <div>
                        <form className="w-100 px-5">
                            {this.state.Meal.MealId === 0 ?
                                <h1 className="mt-5">Ajout d'un plat</h1>
                                : <h1 className="mt-5">Édition d'un plat</h1>}

                            <input type="hidden" name="mealid" className="form-control" value={this.state.Meal.MealId} />
                            <div className="mt-3">
                                <label className="h3 form-label">Libelé du plat</label>
                                <input type="text" name="mealname" className="form-control" value={this.state.Meal.MealName}
                                    onChange={e => this.onMealNameChange(e.currentTarget.value)} />
                            </div>
                            <div className="mt-3">
                                <label className="h3 form-label">Famille du plat</label>
                                <select name="categoryid" className="form-control" onChange={e => this.onCategoryChange(e.target.value)}>
                                    {this.state.categoriesList.map(category =>
                                        this.state.Meal.CategoryId === category.CategoryId
                                            ? <option selected="selected" value={category.CategoryId}>{category.CategoryName}</option>
                                            : <option value={category.CategoryId}>{category.CategoryName} </option>)
                                    }
                                </select>
                            </div>
                            <div className="mt-3">
                                <label className="h3 form-label">Fournisseur</label>
                                <select name="providerid" className="form-control" onChange={e => this.onProviderChange(e.target.value)}>
                                    {this.state.providersList.map(provider =>
                                        this.state.Meal.ProviderId === provider.ProviderId
                                            ? <option selected="selected" value={provider.ProviderId}>{provider.ProviderName}</option>
                                            : <option value={provider.ProviderId}>{provider.ProviderName}</option>)
                                    }
                                </select>
                            </div>
                            <div className="mt-3">
                                <label className="h3 form-label">Prix</label>
                                <input type="number" className="form-control" name="price" min="1" max="99.99" step="0.01" maxlenght="5" value={this.state.Meal.Price}
                                    onChange={e => this.onPriceChange(e.target.value)} />
                            </div>
                            <button onClick={this.handleSubmit} className="btn btn-dark btn-lg w-100 mt-5" type="button">
                                Sauvegarder
                            </button>
                        </form>
                    </div>
                </Modal>

                <Modal show={this.state.showError} handleClose={this.hideModal}>
                    <div className="w-100 px-5">
                        <h1 className="mt-5">Erreur</h1>
                        <p>Veuillez remplir tous les champs</p>
                    </div>
                </Modal>

                <Modal showOk={this.state.showSucces} show={this.state.showSucces} handleClose={this.hideModal}>
                    <div className="w-100 px-5">
                        <h1 className="mt-5">Opération réussie !</h1>
                        <p>Le plat a bien été sauvegardé.</p>
                    </div>
                </Modal>

                <Modal showOk={this.state.showDel} show={this.state.showDel} handleClose={this.hideModal}>
                    <div className="w-100 px-5">
                        <h1 className="mt-5">Opération réussie !</h1>
                        <p>Le(s) plat(s) a/ont bien été supprimé(s).</p>
                    </div>
                </Modal>

                <Modal showOk={this.state.showErrorBack} show={this.state.showErrorBack} handleClose={this.hideModal}>
                    <div className="w-100 px-5">
                        <h1 className="mt-5">Erreur</h1>
                        <p>Une erreur est survenue.</p>
                    </div>
                </Modal>

                <Modal show={this.state.showConfirmDel} handleClose={this.hideModal}>
                    <div className="w-100 px-5">
                        <h1 className="mt-5">Suppression</h1>
                        <p>Voulez vous vraiment supprimer le(s) plat(s) ?</p>
                        <button onClick={this.handleDel} className="btn btn-dark btn-lg w-100 mt-5" type="button">
                            Oui
                        </button>
                    </div>
                </Modal>
            </div>
        );
    }

    async populateMealsData() {
        const response = await fetch('meal');
        const data = await response.json();
        this.setState({ listMeals: data, loading: false });
    }

    async populateProviders() {
        const response = await fetch('provider');
        const data = await response.json();
        this.setState({ providersList: data });
    }

    async populateCategories() {
        const response = await fetch('category');
        const data = await response.json();
        this.setState({ categoriesList: data });
    }
}