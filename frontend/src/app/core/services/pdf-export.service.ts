import { Injectable } from '@angular/core';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { Smartphone } from '../models/smartphone.model';

@Injectable({ providedIn: 'root' })
export class PdfExportService {

  exportCatalogue(smartphones: Smartphone[]): void {
    const doc = new jsPDF();

    doc.setFontSize(18);
    doc.text('Catalogue Smartphones', 14, 18);
    doc.setFontSize(10);
    doc.setTextColor(120);
    doc.text(`Généré le ${new Date().toLocaleDateString('fr-FR')} — ${smartphones.length} produit(s)`, 14, 25);

    autoTable(doc, {
      startY: 32,
      head: [['Marque', 'Modèle', 'Prix (€)', 'Stock', 'Description']],
      body: smartphones.map(s => [
        s.marque,
        s.modele,
        s.prix.toFixed(2),
        s.stock.toString(),
        s.description || '-'
      ]),
      headStyles: { fillColor: [108, 93, 240] },
      styles: { fontSize: 9 },
      alternateRowStyles: { fillColor: [245, 245, 250] }
    });

    doc.save(`catalogue-smartphones-${Date.now()}.pdf`);
  }

  exportFiche(s: Smartphone): void {
    const doc = new jsPDF();

    doc.setFontSize(20);
    doc.text(`${s.marque} ${s.modele}`, 14, 20);

    doc.setDrawColor(108, 93, 240);
    doc.setLineWidth(1);
    doc.line(14, 25, 196, 25);

    doc.setFontSize(12);
    let y = 38;
    const rows: [string, string][] = [
      ['Marque', s.marque],
      ['Modèle', s.modele],
      ['Prix', `${s.prix.toFixed(2)} €`],
      ['Stock disponible', `${s.stock} unité(s)`],
    ];
    rows.forEach(([label, value]) => {
      doc.setTextColor(120);
      doc.text(label, 14, y);
      doc.setTextColor(20);
      doc.text(value, 70, y);
      y += 9;
    });

    if (s.description) {
      y += 4;
      doc.setTextColor(120);
      doc.text('Description :', 14, y);
      y += 7;
      doc.setTextColor(20);
      const lines = doc.splitTextToSize(s.description, 180);
      doc.text(lines, 14, y);
    }

    doc.save(`fiche-${s.marque}-${s.modele}.pdf`.replace(/\s+/g, '-'));
  }
}