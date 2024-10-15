import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {Guid} from 'guid-typescript';
import {saveAs} from 'file-saver';
import {
    FeatherDeviceRecipeDto,
    LedColorDto,
    RecipeClient,
    XivotecRecipeDto
} from "../../../services/apiClient/api.service";

@Component({
    selector: 'app-recipe-detail',
    templateUrl: './recipe-detail.component.html',
    styleUrl: './recipe-detail.component.css'
})
export class RecipeDetailComponent {
    private readonly isNewRecipe: boolean;
    public readonly LedColorDto = LedColorDto;
    public selectedRecipe: XivotecRecipeDto = {
        id: Guid.create().toString(),
        name: "",
        featherDeviceRecipe: {
            id: Guid.create().toString(),
            name: "",
            interval: 0,
            ledColor: LedColorDto.Blue,
            isLedSwitchedOn: false,
            isDisplaySwitchedOn: false,
        }
    };
    public saveButtonText: string;
    public isRecipeInvalid: boolean = false;


    public constructor(private router: Router, private recipeClient: RecipeClient) {
        if ((this.router.getCurrentNavigation()?.extras.state?.['currentrecipe'] != null)
            && this.router.getCurrentNavigation()?.extras.state?.['isupdating'] != null) {
            this.selectedRecipe = this.router.getCurrentNavigation()?.extras.state?.['currentrecipe'];
            this.saveButtonText = "Update";
            this.isNewRecipe = false;
        } else if ((this.router.getCurrentNavigation()?.extras.state?.['currentrecipe'] != null) && (this.router.getCurrentNavigation()?.extras.state?.['isupdating'] == null)) {
            this.selectedRecipe = this.router.getCurrentNavigation()?.extras.state?.['currentrecipe'];
            this.saveButtonText = "Save";
            this.isNewRecipe = true;

            if (!this.isRecipeValid(this.selectedRecipe)) {
                this.isRecipeInvalid = true;
            }
        } else {
            this.saveButtonText = "Save";
            this.isNewRecipe = true;
        }
    }

    public saveButtonClicked(): void {
        const recipe = this.createRecipeDto();

        if (!this.isRecipeValid(recipe)) {
            this.isRecipeInvalid = true;
            return;
        }
        this.isRecipeInvalid = false;

        if (this.isNewRecipe) {
            this.saveRecipe(recipe);
        } else {
            this.updateRecipe(recipe);
        }

        this.router.navigate(['/recipe']);
    }

    public exportButtonClicked(): void {
        const recipe = this.createRecipeDto();
        this.exportRecipe(recipe);
    }

    public importButtonClicked(): void {
        this.importRecipe();
    }

    public onInputNameChange(): void {
        this.isRecipeInvalid = !this.isRecipeValid(this.selectedRecipe);
    }

    private createRecipeDto(): XivotecRecipeDto {
        const featherDeviceRecipeDto: FeatherDeviceRecipeDto = {
            id: this.isNewRecipe ? Guid.create().toString() : this.selectedRecipe.featherDeviceRecipe!.id,
            name: this.selectedRecipe.featherDeviceRecipe!.name,
            interval: this.selectedRecipe.featherDeviceRecipe!.interval,
            ledColor: this.selectedRecipe.featherDeviceRecipe!.ledColor,
            isLedSwitchedOn: this.selectedRecipe.featherDeviceRecipe!.isLedSwitchedOn,
            isDisplaySwitchedOn: this.selectedRecipe.featherDeviceRecipe!.isDisplaySwitchedOn
        };

        return {
            id: this.isNewRecipe ? Guid.create().toString() : this.selectedRecipe.id,
            name: "Xivotec Recipe: " + featherDeviceRecipeDto.name,
            featherDeviceRecipe: featherDeviceRecipeDto
        };
    }

    private saveRecipe(recipe: XivotecRecipeDto): void {
        this.recipeClient.create(recipe)
            .subscribe({
                error: error => console.error(error),
            });
    }

    private updateRecipe(recipe: XivotecRecipeDto): void {
        this.recipeClient.update(recipe)
            .subscribe({
                error: error => console.error(error),
            });
    }

    private exportRecipe(recipe: XivotecRecipeDto): void {
        const dtoJson = JSON.stringify(recipe);
        const blob = new Blob([dtoJson], {type: 'application/json'});
        saveAs(blob, recipe.featherDeviceRecipe!.name + ".json");
    }

    private importRecipe(): void {
        this.router.navigate(['/recipeimport'])
    }

    private isRecipeValid(recipe: XivotecRecipeDto): boolean {
        return recipe.featherDeviceRecipe!.name.trim().length > 0;
    }
}
