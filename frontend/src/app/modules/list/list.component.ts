import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService, ConfirmationService } from 'primeng/api';
import { Smartphone } from '../../core/models/smartphone.model';
import { SmartphoneService } from '../../core/services/smartphone.service';
import { PdfExportService } from '../../core/services/pdf-export.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  smartphones: Smartphone[] = [];
  loading = true;

  constructor(
    private smartphoneService: SmartphoneService,
    private router: Router,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private pdfExportService: PdfExportService
  ) {}

  ngOnInit(): void {
    this.loadSmartphones();
  }

  loadSmartphones(): void {
    this.loading = true;
    this.smartphoneService.getAll().subscribe({
      next: (data) => {
        this.smartphones = data;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Impossible de charger les smartphones.' });
        this.loading = false;
      }
    });
  }

  onAdd(): void {
    this.router.navigate(['/smartphones/add']);
  }

  onEdit(id: string | undefined): void {
    if (id) this.router.navigate(['/smartphones/edit', id]);
  }

  onDelete(smartphone: Smartphone): void {
    this.confirmationService.confirm({
      message: `Supprimer "${smartphone.marque} ${smartphone.modele}" ?`,
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.smartphoneService.delete(smartphone.id!).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Succès', detail: 'Smartphone supprimé.' });
            this.loadSmartphones();
          },
          error: (err) => {
            console.error(err);
            this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Suppression impossible.' });
          }
        });
      }
    });
  }

  onExportPdf(): void {
    this.pdfExportService.exportCatalogue(this.smartphones);
  }

  onExportFiche(smartphone: Smartphone): void {
    this.pdfExportService.exportFiche(smartphone);
  }
}