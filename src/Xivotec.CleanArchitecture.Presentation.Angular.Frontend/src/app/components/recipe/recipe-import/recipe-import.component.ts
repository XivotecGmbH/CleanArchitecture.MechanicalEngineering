import {Component} from '@angular/core';
import {Router} from '@angular/router';

@Component({
    selector: 'app-recipe-import',
    templateUrl: './recipe-import.component.html',
    styleUrl: './recipe-import.component.css'
})
export class RecipeImportComponent {
    public errorText: string = '';

    public constructor(private router: Router) {
    }

    public applyButtonClicked(): void {
        this.router.navigate(['/recipedetail'])
    }

    public onFileChangedEvent(event: Event): void {
        const input = event.target as HTMLInputElement;
        const file = input.files?.[0];

        if (file) {
            const reader = new FileReader();
            reader.onload = (e) => {
                try {
                    const recipe = JSON.parse(e.target?.result as string);
                    this.router.navigate(['/recipedetail'], {state: {currentrecipe: recipe}});
                } catch (error) {
                    this.errorText = "Invalid file";
                }
            };

            reader.readAsText(file);
        }
    }
}
